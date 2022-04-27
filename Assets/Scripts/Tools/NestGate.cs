using UnityEngine;

[RequireComponent(typeof(NestGateAnimator))]
public class NestGate : Gate
{
    private NestGateAnimator _nestGateAnimator;

    private void Awake()
    {
        _nestGateAnimator = GetComponent<NestGateAnimator>();
    }

    public void GiveNest()
    {
        _nestGateAnimator.DeleteCurrentNest();
    }
}
