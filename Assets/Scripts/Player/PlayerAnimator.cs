using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string MOVE = "Move";
    private const string TAKE = "Take";

    public void Move()
    {
        _animator.SetTrigger(MOVE);
    }

    public void Take()
    {
        _animator.SetTrigger(TAKE);
    }
}
