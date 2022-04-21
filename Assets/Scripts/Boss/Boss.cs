using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Boss : Dinosaur
{
    [SerializeField] private Transform _regDoll;
    [SerializeField] private Animator _animator;
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;

    private List<DinoMini> _dinos = new List<DinoMini>();
    private bool _isReadyToAttack = false;
    private int _typeAttack;

    private const float DELAY_BETWEN_ATTACK = 2f;
    private const int SUPER_ATTACK = 2;
    private const float DELAY = 5f;
    private const string WIN = "Win";
    private const string ATTACK = "Attack";
    private const string ATTACKS_TYPE = "AttacksType";

    public float HealthMax { get; private set; }

    public event Action Won;
    public event Action Died;
    public event Action HealthChanged;

    private void OnEnable()
    {
        _bossAreaTrigger.BossAreaReached += Init;
    }

    private void OnDisable()
    {
        _bossAreaTrigger.BossAreaReached -= Init;        
    }

    private IEnumerator AttackRepeat()
    {
        var waitForSeconds = new WaitForSeconds(DELAY_BETWEN_ATTACK);

        while (_isReadyToAttack)
        {
            PlayAttackAnimation();
            yield return waitForSeconds;
        }        
    }

    public void AddDinos(DinoMini dino)
    {
        _dinos.Add(dino);
    }

    public void TakeDamage(float damage)
    {
        if(damage > Health)
            Die();

        Health -= damage;

        if(Health <= 0)
            Die();

        HealthChanged?.Invoke();
    }

    public void Attack()
    {
        if(_typeAttack == SUPER_ATTACK)
        {
            var targets = _dinos.Where(dino => dino.IsAlive).ToList();
            targets.ForEach(dino => dino.TakeDamage(Damage / 4));
        }
        else
        {
            var target = _dinos.OrderByDescending(dino => dino.Health).FirstOrDefault(dino => dino.IsAlive);
            target.TakeDamage(Damage);

            Vector3 lookPosition = new Vector3(_regDoll.position.x, target.transform.position.y, _regDoll.position.z);
            _regDoll.LookAt(lookPosition);
        }
    }

    private void SetHealth()
    {
        Health = _dinos.Sum(dino => dino.Damage) * 3;
        HealthMax = Health;
    }

    private void SetDamage()
    {
        Damage = _dinos.Sum(dino => dino.Health) / _dinos.Count;
    }

    private void Init()
    {
        SetHealth();
        SetDamage();
        Invoke(nameof(StartAttackAnimation), DELAY);
    }

    private void StartAttackAnimation()
    {
        _isReadyToAttack = true;
        StartCoroutine(AttackRepeat());
    }

    private void PlayAttackAnimation()
    {
        var isWon = _dinos.Any(dino => dino.IsAlive);

        if (isWon == false)
        {
            Win();
            return;
        }

        int random = Random.Range(0, 3);
        _typeAttack = random;

        _animator.SetTrigger(ATTACK);
        _animator.SetFloat(ATTACKS_TYPE, random);
    }

    private void Win()
    {
        _isReadyToAttack = false;
        _animator.SetTrigger(WIN);
        Won?.Invoke();
    }

    private void Die()
    {
        OnDied();
        _isReadyToAttack = false;
        _animator.enabled = false;
        enabled = false;
        Died?.Invoke();
    }
}
