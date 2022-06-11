using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipDetector3D : MonoBehaviour
{
    public float sizeX = 10;
    public float sizeY = 10;
    public float sizeZ = 10;

    HasToolTip _obj;

    private void Awake()
    {
        if (GetComponent<Collider>() == null)
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.size = new Vector3(sizeX, sizeY, sizeZ);
        }

        _obj = GetComponentInChildren<HasToolTip>();
    }

    public void OnMouseOver()
    {
        _obj.ShowToolTip();
    }

    public void OnMouseExit()
    {
        _obj.HideToolTip();
    }
}
