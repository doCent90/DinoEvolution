using UnityEngine;
using DG.Tweening;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [Header("Settings")]

    private float _offset;
    private bool _hasSortGate = false;

    private const float Treshold = 1f;
    private const float Duration = 0.3f;

    public Vector3 Position { get; private set; }

    public void SetDefaultPosition()
    {
        _hasSortGate = true;
        transform.DOMoveX(0, Duration);
    }

    private void Move()
    {
        _offset = _inputController.CurrentOffset;
        float position = Mathf.Clamp(_offset, -Treshold, Treshold);

        Vector3 targetPosition = new Vector3(position, transform.position.y, transform.position.z);
        transform.position = targetPosition;

        Position = transform.position;
    }

    private void Update()
    {
        if (_hasSortGate)
            return;

        Move();
    }
}
