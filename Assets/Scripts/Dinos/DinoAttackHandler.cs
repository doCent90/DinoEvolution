using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DinoAttackHandler : MonoBehaviour
{
    [SerializeField] private Dino _dino;

    private void Attack()  // Invoke from animation clip.
    {
        _dino.Attack();
    }
}
