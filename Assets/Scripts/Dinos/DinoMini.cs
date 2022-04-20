using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(DinoAnimator))]
public class DinoMini : Dinosaur
{
    private Boss _boss;
    private BossArea _bossArea;
    private DinoAnimator _dinoAnimator;
    private NavMeshAgent _navMeshAgent;

    private const float DELAY = 5f;

    public UI UI { get; private set; }
    public bool IsAlive { get; private set; } = true;

    public event Action Died;

    private void OnEnable()
    {
        _dinoAnimator = GetComponent<DinoAnimator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        _boss.Died -= Win;
        UI.FightClicked -= PlayAttackAnimation;
    }

    public void Init(BossArea bossArea, float health, float damage)
    {
        _bossArea = bossArea;
        UI = bossArea.UI;

        Health = health;
        Damage = damage;

        GoToBossArea();
        _boss = bossArea.Boss;
        _boss.AddDinos(this);

        _boss.Died += Win;
        UI.FightClicked += PlayAttackAnimation;
    }

    public void TakeDamage(float damage)
    {
        if (damage > Health)
        {
            Die();
        }
        else
        {
            Health -= damage;

            if (Health < 0)
                Die();
        }

        _dinoAnimator.PlayHit();
    }

    public void PlayAttackAnimation()
    {
        _dinoAnimator.PlayAttack();
    }

    public void Attack()
    {
        if(IsAlive)
            _boss.TakeDamage(Damage);
    }

    private void Die()
    {
        OnDied();
        IsAlive = false;
        _dinoAnimator.enabled = false;
        enabled = false;
        Died?.Invoke();
    }

    private void Win()
    {
        GoToFinishPoint();
    }

    private void GoToBossArea()
    {
        Transform target = _bossArea.GetPosition();
        GoToPosition(target);

        Invoke(nameof(GoToBoss), DELAY);
    }

    private void GoToBoss()
    {
        Transform target = _bossArea.Boss.transform;
        GoToPosition(target);
    }

    private void GoToFinishPoint()
    {
        Vector3 target = _bossArea.GetFiniPointPosition();
        GoToPosition(target);
    }

    private void GoToPosition(Transform target)
    {
        Vector3 direction = target.position;
        _navMeshAgent.destination = direction;
    }

    private void GoToPosition(Vector3 target)
    {
        Vector3 direction = target;
        _navMeshAgent.destination = direction;
    }
}
