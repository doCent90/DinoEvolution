using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float _sensitivity;
    [SerializeField] private PlayerMover _playerMover;

    private float _saveOffset;
    private Vector3 _mousePosition;

    private const float Multiply = 100f;

    public float CurrentOffset { get; private set; }

    public event Action Clicked;

    private void OnEnable()
    {
        _sensitivity *= Multiply;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Clicked?.Invoke();

        GetOffsetValue();
    }

    public void GetOffsetValue()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _saveOffset = _playerMover.Position.x;
            _mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 offset = Input.mousePosition - _mousePosition;
            CurrentOffset = _saveOffset + offset.x / _sensitivity;
        }
    }
}
