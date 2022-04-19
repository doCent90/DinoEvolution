using UnityEngine;
using UnityEngine.AI;

public class DinoAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private float _spentTime;
    private bool _isReadyToAttack = false;
    private string[] _attcakAnimations = new string[2];

    private float _delayBetwinAttcak = 2f;
    private readonly float _minSpeed = 0.5f;

    private const string RUN = "Run";
    private const string WIN = "Win";
    private const string ATTACK1 = "Attack1";
    private const string ATTACK2 = "Attack2";

    private void OnEnable()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _attcakAnimations[0] = ATTACK1;
        _attcakAnimations[1] = ATTACK2;
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
            int random = Random.Range(0, 2);
            _animator.SetTrigger(_attcakAnimations[random]);
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
