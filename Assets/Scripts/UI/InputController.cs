using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private float _axisX;

    private const string MouseX = "Mouse X";

    public event Action Clicked;

    public float GetSlideValue()
    {
        if (Input.GetMouseButton(0))
        {
            _axisX += Input.GetAxis(MouseX);
        }

        return _axisX;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Clicked?.Invoke();
    }
}
