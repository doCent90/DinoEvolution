using UnityEngine;

[RequireComponent(typeof(NestGateAnimator))]
public class NestGate : MonoBehaviour
{
    private NestGateAnimator _nestGateAnimator;

    public void GiveNest()
    {
        _nestGateAnimator.DeleteCurrentNest();
    }

    private void OnEnable()
    {
        _nestGateAnimator = GetComponent<NestGateAnimator>();
    }
}
