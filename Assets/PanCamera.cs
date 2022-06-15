using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    public Transform menuA, menuB;
    public GameObject inputField;
    public float speed;

    bool _a = true;
    bool _moving = false;
    float _t = 0;

    public void Toggle()
    {
        if (_moving) return;

        //For some reason, input field hides some ui when it's enabled
        //Just a simple workaround
        if (_a) inputField.SetActive(false);

        _a = !_a;
        _moving = true;
    }

    private void Update()
    {
        if (!_moving) return;

        _t += Time.deltaTime * speed;

        Transform src, dest;
        if (_a)
        {
            src = menuB;
            dest = menuA;
        }
        else
        {
            src = menuA;
            dest = menuB;
        }

        if (_t >= 1) {
            _moving = false;
            transform.position = dest.position;
            _t = 0;
            if (_a) inputField.SetActive(true);
        }
        else
        {
            float t = (-Mathf.Cos(_t * Mathf.PI) + 1) / 2;
            transform.position = Vector3.Lerp(src.position, dest.position, t);
        }
    }
}
