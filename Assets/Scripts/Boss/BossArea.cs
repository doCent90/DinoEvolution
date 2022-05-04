using UnityEngine;

public class BossArea : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private UI _uI;
    [Header("Random finish point")]
    [SerializeField] private float _range;
    [SerializeField] private Transform _finishPoint;
    [Header("Point on Boss Area")]
    [SerializeField] private Transform[] _points;

    private int _count;

    public UI UI => _uI;
    public Boss Boss => _boss;

    public Transform GetPosition()
    {
        return _points[_count++];
    }

    public Vector3 GetFiniPointPosition()
    {
        float x = Random.Range(-_range, _range);
        float z = Random.Range(-_range, _range);

        Vector3 origPosition = _finishPoint.position;
        Vector3 randomPosition = new Vector3(origPosition.x + x, origPosition.y, origPosition.z + z);
        return randomPosition;
    }
}
