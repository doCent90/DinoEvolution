using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [Header("Settings")]
    [Range(0f, 0.1f)]
    [SerializeField] private float _sensitivity = 0.08f;

    private float _slideDirection;

    private const float TRESHOLD = 1f;

    private void Move()
    {
        _slideDirection = _inputController.GetSlideValue() * _sensitivity;

        var position = Mathf.Clamp(_slideDirection, -TRESHOLD, TRESHOLD);
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        Move();
    }
}
