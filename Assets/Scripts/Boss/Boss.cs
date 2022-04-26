using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Boss : Dinosaur
{
    [SerializeField] private UI _uI;
    [SerializeField] private Transform _regDoll;
    [SerializeField] private Animator _animator;
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;

    private List<DinoMini> _dinos = new List<DinoMini>();
    private bool _isReadyToAttack = false;
    private bool _isAlive = true;
    private int _typeAttack;

    private const float SuperAttackPower = 0.25f;
    private const float DelayBetwenAttack = 2f;
    private const int SuperAttack = 2;
    private const int HeatlthMultiply = 4;
    private const string WinAnimation = "Win";
    private const string AttackAnimation = "Attack";
    private const string AttackAnimationsType = "AttacksType";

    public float HealthMax { get; private set; }

    public event Action Won;
    public event Action Died;
    public event Action HealthChanged;
    public event Action SuperAttacked;

    private void OnEnable()
    {
        _uI.FightClicked += StartAttackAnimation;
        _bossAreaTrigger.BossAreaReached += Init;
    }

    private void OnDisable()
    {
        _bossAreaTrigger.BossAreaReached -= Init;        
    }

    private IEnumerator AttackRepeat()
    {
        var waitForSeconds = new WaitForSeconds(DelayBetwenAttack);

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
        if (_isAlive == false)
            return;

        if(damage > Health && Health > 0)
            Die();

        if (Health > 0)
            Health -= damage;

        if(Health <= 0)
            Die();

        HealthChanged?.Invoke();
    }

    public void Attack()
    {
        if(_typeAttack == SuperAttack)
        {
            var targets = _dinos.Where(dino => dino.IsAlive).ToList();
            targets.ForEach(dino => dino.TakeDamage(Damage * SuperAttackPower));
            SuperAttacked?.Invoke();
        }
        else
        {
            DinoMini target = _dinos.OrderByDescending(dino => dino.Health).FirstOrDefault(dino => dino.IsAlive);
            target.TakeDamage(Damage);

            Vector3 lookPosition = new Vector3(_regDoll.position.x, target.transform.position.y, _regDoll.position.z);
            _regDoll.LookAt(lookPosition);
        }
    }

    private void SetHealth()
    {
        Health = _dinos.Sum(dino => dino.Damage) * HeatlthMultiply;
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
    }

    private void StartAttackAnimation()
    {
        _isReadyToAttack = true;
        StartCoroutine(AttackRepeat());
        _uI.FightClicked -= StartAttackAnimation;
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

        _animator.SetTrigger(AttackAnimation);
        _animator.SetFloat(AttackAnimationsType, random);
    }

    private void Win()
    {
        _isReadyToAttack = false;
        _animator.SetTrigger(WinAnimation);
        Won?.Invoke();
    }

    private void Die()
    {
        OnDied();
        _isReadyToAttack = false;
        _animator.enabled = false;
        _isAlive = false;
        enabled = false;
        Died?.Invoke();
    }
}
