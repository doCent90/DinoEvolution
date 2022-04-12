using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FinishTrigger finishTrigger))
            _player.DisableMove();
    }
}
