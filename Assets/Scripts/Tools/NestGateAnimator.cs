using UnityEngine;
using DG.Tweening;

public class NestGateAnimator : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _nests;

    private readonly float _distance = -0.8f;
    private int _count;

    private const float Duration = 0.1f;

    public void DeleteCurrentNest()
    {
        _nests[_count].enabled = false;
        _count++;

        foreach (MeshRenderer nest in _nests)
        {
            Vector3 currentNest = nest.transform.localPosition;
            Vector3 target = new Vector3(currentNest.x + _distance, currentNest.y, currentNest.z);
            nest.transform.DOLocalMove(target, Duration);
        }
    }
}
