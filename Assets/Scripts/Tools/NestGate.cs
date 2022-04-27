using UnityEngine;

[RequireComponent(typeof(NestGateAnimator))]
public class NestGate : Gate
{
    private NestGateAnimator _nestGateAnimator;

    private void OnEnable()
    {
        _nestGateAnimator = GetComponent<NestGateAnimator>();
    }

    public void GiveNest()
    {
        _nestGateAnimator.DeleteCurrentNest();
    }
}
