using UnityEngine;

[RequireComponent(typeof(NestGateAnimator))]
public class NestGate : MonoBehaviour
{
    private NestGateAnimator _nestGateAnimator;

    private void OnEnable()
    {
        _nestGateAnimator = GetComponent<NestGateAnimator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg) && egg.HaveNest == false)
            _nestGateAnimator.DeleteCurrentNest();
    }
}
