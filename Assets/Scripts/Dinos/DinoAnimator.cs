using UnityEngine;
using UnityEngine.AI;

public class DinoAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _blinkMaterial;
    [SerializeField] private ParticleSystem _particleSystem;

    private Material _originalMaterial;
    private NavMeshAgent _navMeshAgent;

    private float _spentTime;
    private bool _isReadyToAttack = false;

    private readonly float _minSpeed = 0.5f;

    private readonly float DELAY = 0.2f;
    private readonly float DELAY_BETWEN_ATTACK = 2f;
    private const string RUN = "Run";
    private const string WIN = "Win";
    private const string ATTACK = "Attack";
    private const string ATTACKS_TYPE = "AttacksType";

    private void OnEnable()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _originalMaterial = _renderer.material;
    }

    private void OnDisable()
    {
        _animator.enabled = false;
    }

    private void Update()
    {        
        SetRun(_navMeshAgent.velocity);

        _spentTime += Time.deltaTime;

        if(_spentTime > DELAY_BETWEN_ATTACK)
            _isReadyToAttack = true;
        else
            _isReadyToAttack = false;
    }

    public void PlayHit()
    {
        Blink();
        _particleSystem.Play();
    }

    public void PlayAttack()
    {
        if (_isReadyToAttack)
        {
            _spentTime = 0;
            int random = Random.Range(0, 2);

            _animator.SetTrigger(ATTACK);
            _animator.SetFloat(ATTACKS_TYPE, random);
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

    private void Blink()
    {
        _renderer.material = _blinkMaterial;
        Invoke(nameof(SetOriginalMaterial), DELAY);
    }

    private void SetOriginalMaterial()
    {
        _renderer.material = _originalMaterial;
    }
}
