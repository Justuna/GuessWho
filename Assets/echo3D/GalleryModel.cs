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

public class GalleryModel : MonoBehaviour, HasToolTip
{
    [HideInInspector]
    public Entry entry;

    [HideInInspector]
    public List<string> Names { get; private set; }
    [HideInInspector]
    public List<string> Properties { get; private set; }

    int _children = 0;
    List<MeshRenderer> _renderers = new List<MeshRenderer>();

    bool _display = false;
    bool _displayed = false;

    // Use this for initialization
    void Start()
    {
        GameObject wrapper = new GameObject(name + " wrapper");
        transform.SetParent(wrapper.transform);

        RemoteTransformations rt = this.gameObject.AddComponent<RemoteTransformations>();
        rt.entry = entry;
        rt.zeroOut = true;

        //added to parent to avoid remotetransform interference
        ToolTipDetector3D tt = wrapper.AddComponent<ToolTipDetector3D>();

        Names = new List<string>();
        string allNames = ((ModelHologram)entry.getHologram()).getFilename().Split('.')[0];
        foreach (string name in allNames.Split(' ')) Names.Add(name.Replace('_', ' '));

        Properties = new List<string>();
        string value;
        if (entry.getAdditionalData() != null && entry.getAdditionalData().TryGetValue("guesswho", out value))
        {
            Debug.Log(value);
            foreach (string prop in value.Split(' ')) Properties.Add(prop.Replace('_', ' '));
        }

        GalleryManager.Instance.AddModel(this);
    }

    private void Update()
    {
        if (_children != this.transform.childCount)
        {
            _children = this.transform.childCount;
            CacheRenderers();
        }
        if (_displayed != _display)
        {
            _displayed = _display;
            foreach (MeshRenderer renderer in _renderers)
            {
                renderer.enabled = _display;
            }
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
                renderer.enabled = _display;
            }
        }
    }

    public void Hide()
    {
        _display = false;
    }

    public void Display()
    {
        _display = true;
    }

    public void Toggle()
    {
        _display = !_display;
    }

    public void ShowToolTip()
    {
        string tooltip = "Name: " + Names[0];
        if (Names.Count > 1)
        {
            tooltip += "\nAKA: ";
            for (int i = 1; i < Names.Count - 1; i++)
            {
                tooltip += "\n" + Names[i] + ", ";
            }
            tooltip += "\n" + Names[Names.Count - 1];
        }
        tooltip += "\nProperties: ";
        for (int i = 0; i < Properties.Count - 1; i++)
        {
            tooltip += "\n" + Properties[i] + ", ";
        }
        tooltip += "\n" + Properties[Properties.Count - 1];
        ToolTip.ShowTooltip(tooltip);
    }

    public void HideToolTip()
    {
        ToolTip.HideTooltip();
    }
}
