using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(DinoAnimator))]
public class DinoMini : Dinosaur
{
    private UI _uI;
    private Boss _boss;
    private BossArea _bossArea;
    private GameOver _gameOver;
    private DinoAnimator _dinoAnimator;
    private NavMeshAgent _navMeshAgent;

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
        _gameOver.Won -= Win;
        _uI.FightClicked -= GoToBoss;
        _uI.FightClicked -= PlayAttackAnimation;
    }

    public void Init(GameOver gameOver, BossArea bossArea, float health, float damage)
    {
        _bossArea = bossArea;
        _uI = bossArea.UI;
        _gameOver = gameOver;
        _boss = bossArea.Boss;

        Health = health;
        Damage = damage;

        GoToBossArea();
        _boss.AddDinos(this);

        _boss.Died += Win;
        _gameOver.Won += Win;
        _uI.FightClicked += GoToBoss;
        _uI.FightClicked += PlayAttackAnimation;
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

        _dinoAnimator.PlayHit(_boss);
    }

    public void PlayAttackAnimation()
    {
        _dinoAnimator.OnAttack();
    }

    public void Attack()
    {
        if(IsAlive)
            _boss.TakeDamage(Damage);
    }

    private void Die()
    {
        _dinoAnimator.DisableShadow();

        _navMeshAgent.enabled = false;
        _dinoAnimator.enabled = false;

        IsAlive = false;
        enabled = false;

        OnDied();
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
