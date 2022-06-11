using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public static ToolTip Instance { get; private set; }

    public RectTransform background;
    public TextMeshProUGUI text;

    public float padding;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.transform.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
    }

    void _ShowTooltip(string msg)
    {
        gameObject.SetActive(true);

        text.text = msg;
        Vector2 bgSize = new Vector2(text.preferredWidth + padding * 2, text.preferredHeight + padding * 2);
        background.sizeDelta = bgSize;
    }

    void _HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip(string msg)
    {
        Instance._ShowTooltip(msg);
    }

    public static void HideTooltip()
    {
        Instance._HideTooltip();
    }
}
