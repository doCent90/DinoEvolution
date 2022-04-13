using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinalPlace : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private BoxCollider _boxCollider;

    public Transform GetPlacePosition()
    {
        _boxCollider.enabled = false;
        return _point;
    }

    private void OnEnable()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
}
