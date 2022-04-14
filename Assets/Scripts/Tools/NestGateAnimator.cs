using UnityEngine;
using DG.Tweening;

public class NestGateAnimator : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _nests;
    [SerializeField] private float _distance;

    private int _count;

    private const float DURATION = 0.1f;

    public void DeleteCurrentNest()
    {
        _nests[_count].enabled = false;
        _count++;

        foreach (var nest in _nests)
        {
            Vector3 currentNest = nest.transform.localPosition;
            Vector3 target = new Vector3(currentNest.x + _distance, currentNest.y, currentNest.z);
            nest.transform.DOLocalMove(target, DURATION);
        }
    }
}
