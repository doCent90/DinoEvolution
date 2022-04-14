using UnityEngine;
using DG.Tweening;

public class SortGate : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private const float DURATION = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
            Sort(egg);
    }

    private void Sort(Egg egg)
    {
        if(egg.WasLightsHeated == false || egg.WasWashed == false || egg.HaveNest == false)
        {
            //egg.PlayerHand.DeleteNotCompleteEgg(egg);

            //egg.transform.parent = transform;
            //egg.GetComponent<EggMover>().enabled = false;
            //egg.transform.DOMove(_point.position, DURATION);
        }
    }
}
