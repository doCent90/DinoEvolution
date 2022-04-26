using UnityEngine;
using DG.Tweening;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [Header("Settings")]
    [Range(0f, 0.1f)]
    [SerializeField] private float _sensitivity = 0.03f;

    private float _slideDirection;
    private bool _hasSortGate = false;

    private const float Treshold = 1f;
    private const float Duration = 1f;

    public void SetDefaultPosition()
    {
        _hasSortGate = true;
        transform.DOMoveX(0, Duration);
    }

    private void Move()
    {
        _slideDirection = _inputController.GetSlideValue() * _sensitivity;

        float position = Mathf.Clamp(_slideDirection, -Treshold, Treshold);
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (_hasSortGate)
            return;

        Move();
    }
}
