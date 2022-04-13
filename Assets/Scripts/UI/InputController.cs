using UnityEngine;

public class InputController : MonoBehaviour
{
    private float _axisX;

    private const string MouseX = "Mouse X";

    public float GetSlideValue()
    {
        if (Input.GetMouseButton(0))
        {
            _axisX += Input.GetAxis(MouseX);
        }

        return _axisX;
    }
}
