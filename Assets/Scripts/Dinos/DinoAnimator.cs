using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class DinoAnimator : MonoBehaviour
{
    [Range(0f, 0.1f)]
    [SerializeField] private float _blinkTime = 0.05f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private ParticleSystem _particleSystem;

    private NavMeshAgent _navMeshAgent;

    private float _spentTime;
    private bool _isReadyToAttack = false;

    private const float ScaleDuration = 0.5f;
    private const float MinScale = 0.1f;
    private const float MinSpeed = 0.6f;
    private const float DelayBetwenAttack = 1f;
    private const float DelayBeforeAttack = 1f;
    private const string Run = "Run";
    private const string Win = "Win";
    private const string Attack = "Attack";
    private const string AttacksType = "AttacksType";
    private const string TextureImpact = "_TextureImpact";

    private void OnEnable()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetOriginalScale();
    }

    private void OnDisable()
    {
        _animator.enabled = false;
    }

    private void Update()
    {        
        SetRun(_navMeshAgent.velocity);

        _spentTime += Time.deltaTime;

        if(_spentTime > DelayBetwenAttack)
            _isReadyToAttack = true;
        else
            _isReadyToAttack = false;
    }

    public void PlayHit()
    {
        Blink();
        _particleSystem.Play();
    }

    public void OnAttack()
    {
        float randomSec = Random.Range(0, DelayBeforeAttack);
        Invoke(nameof(PlayAttack), DelayBetwenAttack);
    }

    public void PlayWin()
    {
        _animator.SetTrigger(Win);
    }

    private void PlayAttack()
    {
        if (_isReadyToAttack)
        {
            _spentTime = 0;
            int random = Random.Range(0, 2);

            _animator.SetTrigger(Attack);
            _animator.SetFloat(AttacksType, random);
        }
    }

    private void SetRun(Vector3 velocity)
    {
        float speed = velocity.magnitude;

        if(speed > MinSpeed)
            _animator.SetBool(Run, true);
        else
            _animator.SetBool(Run, false);
    }

    private void SetOriginalScale()
    {
        Vector3 origScale = transform.localScale;
        transform.localScale = new Vector3(MinScale, MinScale, MinScale);
        var tween = transform.DOScale(origScale, ScaleDuration);
    }

    private void Blink()
    {
        var blinkFirstOn = _renderer.material.DOFloat(0.5f, TextureImpact, _blinkTime);
        var blinkFirstOff = _renderer.material.DOFloat(1, TextureImpact, _blinkTime).SetDelay(_blinkTime);
        var blinkSecondOn = _renderer.material.DOFloat(0.8f, TextureImpact, _blinkTime).SetDelay(_blinkTime*2);
        var blinkSecondOff = _renderer.material.DOFloat(1, TextureImpact, _blinkTime).SetDelay(_blinkTime*4);
    }
}
