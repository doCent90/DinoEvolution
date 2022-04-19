using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private UI _uI;
    [SerializeField] private Transform _regDoll;
    [SerializeField] private Animator _animator;
    [SerializeField] private RegDollKicker _regDollKicker;

    private List<Dino> _dinos = new List<Dino>();

    private readonly float _delay = 4f;

    private const string WIN = "Win";
    private const string ATTACK = "Attack";

    public float Health { get; private set; }
    public float HealthMax { get; private set; }
    public float Damage { get; private set; }

    public event Action Won;
    public event Action Died;
    public event Action HealthChanged;

    private void OnEnable()
    {
        _uI.TapToFightClicked += OnTapToFightClicked;
    }

    private void OnDisable()
    {
        _uI.TapToFightClicked -= OnTapToFightClicked;        
    }

    public void AddDinos(Dino dino)
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
        var isWon = _dinos.Any(dino => dino.IsAlive);

        if (isWon == false)
        {
            Win();
            return;
        }

        var target = _dinos.OrderByDescending(dino => dino.Health).FirstOrDefault(dino => dino.IsAlive);
        target.TakeDamage(Damage);

        Vector3 lookPosition = new Vector3(_regDoll.position.x, target.transform.position.y, _regDoll.position.z);
        _regDoll.LookAt(lookPosition);
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

    private void OnTapToFightClicked()
    {
        SetHealth();
        SetDamage();
        Invoke(nameof(PlayAttackAnimation), _delay);
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger(ATTACK);
    }

    private void Win()
    {
        _animator.SetTrigger(WIN);
        Won?.Invoke();
    }

    private void Die()
    {
        _animator.enabled = false;
        enabled = false;
        _regDollKicker.Kick();
        Died?.Invoke();
    }
}
