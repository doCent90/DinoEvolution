using UnityEngine;

public class BossArea : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Boss _boss;
    [SerializeField] private UI _uI;

    private int _count;

    public UI UI => _uI;
    public Boss Boss => _boss;

    public Transform GetPosition()
    {
        return _points[_count++];
    }
}
