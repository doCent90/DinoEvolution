using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Button _scmoothLow;
    [SerializeField] private Button _scmoothMedium;
    [SerializeField] private Button _scmoothHigh;
    [SerializeField] private InputController _inputController;
    [Header("Settings")]
    [SerializeField] private float _sens = 0.25f;

    private float _offset;
    private bool _hasSortGate = false;

    private const float Treshold = 1f;
    private const float Duration = 0.3f;

    public Vector3 Position { get; private set; }

    private void OnEnable()
    {
        if (_scmoothMedium == null)
            return;

        _scmoothLow.onClick.AddListener(SetSmoothLow);
        _scmoothMedium.onClick.AddListener(SetSmoothMedium);
        _scmoothHigh.onClick.AddListener(SetSmoothHigh);
    }

    private void OnDisable()
    {
        _scmoothLow?.onClick?.RemoveListener(SetSmoothLow);
        _scmoothMedium.onClick?.RemoveListener(SetSmoothMedium);
        _scmoothHigh?.onClick?.RemoveListener(SetSmoothHigh);
    }

    private void Update()
    {
        if (_hasSortGate)
            return;

        Move();
    }

    public void SetDefaultPosition()
    {
        _hasSortGate = true;
        transform.DOMoveX(0, Duration);
        enabled = false;
    }

    private void SetSmoothLow()
    {
        SetSmoothValue(0.25f);
    }

    private void SetSmoothMedium()
    {
        SetSmoothValue(0.20f);
    }

    private void SetSmoothHigh()
    {
        SetSmoothValue(0.15f);
    }

    private void SetSmoothValue(float sens)
    {
        _sens = sens;
    }

    private void Move()
    {
        _offset = _inputController.CurrentOffset;
        float position = Mathf.Clamp(_offset, -Treshold, Treshold);

        Vector3 targetPosition = new Vector3(position, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _sens);

        Position = transform.position;
    }
}
