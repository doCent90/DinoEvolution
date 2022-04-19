using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField] private Boss _boss;

    private void Attack()
    {
        _boss.Attack(); // Invoke from animation clip.
    }
}
