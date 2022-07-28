using System.Collections.Generic;
using UnityEngine;

public class GalleryTooltip : MonoBehaviour, HasToolTip
{
    [HideInInspector]
    public List<string> names;
    [HideInInspector]
    public List<string> properties;

    public void ShowToolTip()
    {
        string tooltip = "Name: " + names[0];
        if (names.Count > 1)
        {
            tooltip += "\nAKA: ";
            for (int i = 1; i < names.Count - 1; i++)
            {
                tooltip += "\n" + names[i] + ", ";
            }
            tooltip += "\n" + names[names.Count - 1];
        }
        tooltip += "\nProperties: ";
        for (int i = 0; i < properties.Count - 1; i++)
        {
            tooltip += "\n" + properties[i] + ", ";
        }
        tooltip += "\n" + properties[properties.Count - 1];
        ToolTip.ShowTooltip(tooltip);
    }

    public void HideToolTip()
    {
        ToolTip.HideTooltip();
    }
}