using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _animatorHandModel;

    private const string MoveAnimation = "Move";
    private const string JumpAnimation = "Jump";
    private const string TakeAnimation = "Take";

    public void Move()
    {
        _animator.SetTrigger(MoveAnimation);
    }

    public void Take()
    {
        _animator.SetTrigger(TakeAnimation);
    }

    public void Jump()
    {
        _animatorHandModel.SetTrigger(JumpAnimation);
    }
}
