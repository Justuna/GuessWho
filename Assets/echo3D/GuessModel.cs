/**************************************************************************
* Copyright (C) echoAR, Inc. (dba "echo3D") 2018-2021.                    *
* echoAR, Inc. proprietary and confidential.                              *
*                                                                         *
* Use subject to the terms of the Terms of Service available at           *
* https://www.echo3D.co/terms, or another agreement                       *
* between echoAR, Inc. and you, your company or other organization.       *
***************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class GuessModel : MonoBehaviour
{
    [HideInInspector]
    public Entry entry;

    int _children = 0;
    List<MeshRenderer> _renderers;

    void Start()
    {
        _renderers = new List<MeshRenderer>();
        
        this.gameObject.AddComponent<RemoteTransformations>().entry = entry;

        List<string> names = new List<string>();
        string allNames = ((ModelHologram)entry.getHologram()).getFilename().Split('.')[0];
        foreach (string name in allNames.Split(' ')) names.Add(name.Replace('_', ' '));

        List<string> properties = new List<string>();
        string value;
        if (entry.getAdditionalData() != null && entry.getAdditionalData().TryGetValue("guesswho", out value))
        {
            Debug.Log(value);
            foreach (string prop in value.Split(' ')) properties.Add(prop.Replace('_', ' '));
        }

        GameManager.Instance.Setup(names, properties, gameObject);
    }

    private void Update()
    {
        if (_children != this.transform.childCount)
        {
            _children = this.transform.childCount;
            CacheRenderers();
        }
    }

    private void CacheRenderers()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (!_renderers.Contains(renderer))
            {
                _renderers.Add(renderer);
                renderer.enabled = false;
            }
        }
    }

    public void Hide()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.enabled = false;
        }
    }

    public void Reveal()
    {
        foreach(MeshRenderer renderer in _renderers)
        {
            renderer.enabled = true;
        }
    }
}