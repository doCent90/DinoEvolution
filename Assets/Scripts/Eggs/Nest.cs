using UnityEngine;

public class Nest : MonoBehaviour
{
    [SerializeField] private Transform _point;

    public bool IsBusy { get; private set; } = false;

    public Transform GetNestPosition()
    {
        IsBusy = true;
        return _point;
    }
}
