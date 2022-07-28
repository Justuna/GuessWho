/**************************************************************************
*Copyright(C) echoAR, Inc. (dba "echo3D") 2018 - 2021.                    *
*echoAR, Inc.proprietary and confidential.                              *
*                                                                         *
* Use subject to the terms of the Terms of Service available at           *
* https://www.echo3D.co/terms, or another agreement                       *
*between echoAR, Inc.and you, your company or other organization.       *
***************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class RendererGroup : MonoBehaviour
{
    bool _display = false;

    private void Update()
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>(true))
        {
            renderer.enabled = _display;
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
}