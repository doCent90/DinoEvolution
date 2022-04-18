using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class DinoAnimator : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private float _spentTime;
    private bool _isReadyToAttack = false;

    private float _delayBetwinAttcak = 2f;
    private readonly float _minSpeed = 0.5f;

    private const string RUN = "Run";
    private const string WIN = "Win";
    private const string ATTACK = "Attack";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        _animator.enabled = false;
    }

    private void Update()
    {        
        SetRun(_navMeshAgent.velocity);

        _spentTime += Time.deltaTime;

        if(_spentTime > _delayBetwinAttcak)
            _isReadyToAttack = true;
        else
            _isReadyToAttack = false;
    }

    public void PlayAttack()
    {
        if (_isReadyToAttack)
        {
            _spentTime = 0;
            _animator.SetTrigger(ATTACK);
        }
    }

    public void PlayWin()
    {
        _animator.SetTrigger(WIN);
    }

    private void SetRun(Vector3 velocity)
    {
        float speed = velocity.magnitude;

        if(speed > _minSpeed)
            _animator.SetBool(RUN, true);
        else
            _animator.SetBool(RUN, false);
    }
}
