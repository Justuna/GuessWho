using TMPro;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    public Color[] colors;
    public float speed;

    TextMeshProUGUI _textbox;
    float t = 0;

    private void Awake()
    {
        _textbox = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        t += speed * Time.deltaTime;
        if (t >= colors.Length) t = 0;
        int prev = Mathf.FloorToInt(t);
        int next = Mathf.CeilToInt(t);
        if (next >= colors.Length) next = 0;
        _textbox.color = Color.Lerp(colors[prev], colors[next], t % 1);
    }
}
