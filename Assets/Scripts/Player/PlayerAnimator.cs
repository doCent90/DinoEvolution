using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string MoveAnimation = "Move";
    private const string TakeAnimation = "Take";

    public void Move()
    {
        _animator.SetTrigger(MoveAnimation);
    }

    public void Take()
    {
        _animator.SetTrigger(TakeAnimation);
    }
}
