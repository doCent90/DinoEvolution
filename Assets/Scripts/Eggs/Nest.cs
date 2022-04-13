using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Nest : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private BoxCollider _boxCollider;

    public Transform GetNestPosition()
    {
        _boxCollider.enabled = false;
        return _point;
    }

    private void OnEnable()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
}
