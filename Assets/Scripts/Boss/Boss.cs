using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour
{
    [SerializeField] private UI _uI;

    private Animator _animator;
    private List<Dino> _dinos = new List<Dino>();

    private readonly float _delay = 4f;

    private const string WIN = "Win";
    private const string ATTACK = "Attack";

    public float Health { get; private set; }
    public float Damage { get; private set; }

    public event Action Won;
    public event Action Died;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
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
    }

    private void SetHealth()
    {
        Health = _dinos.Sum(dino => dino.Damage) * 3;
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

    private void Attack() // Invoke from animation clip.
    {
        var isWon = _dinos.Any(dino => dino.IsAlive);

        if (isWon == false)
        {
            Win();
            return;
        }

        var target = _dinos.OrderByDescending(dino => dino.Health).FirstOrDefault(dino => dino.IsAlive);
        target.TakeDamage(Damage);

        Vector3 lookPosition = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        transform.LookAt(lookPosition);
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
        Died?.Invoke();
    }
}
