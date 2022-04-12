using System;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private float _health;
    [SerializeField] private float _damage;

    public Player Player { get; private set; }
    public PlayerHand PlayerHand { get; private set; }
    public bool HasInStack { get; private set; } = false;

    public float Health => _health;
    public float Damage => _damage;

    public event Action OtherEggTaked;
    public event Action<Transform> Finished;
    public event Action<Transform, float, float> Taked;

    public void OnTaked(Player player, PlayerHand playerHand, Transform followPosition, float step, float time)
    {
        HasInStack = true;
        Player = player;
        PlayerHand = playerHand;
        Taked?.Invoke(followPosition, step, time);
    }

    public void Animate()
    {
        OtherEggTaked?.Invoke();
    }

    public void MoveToNest(Transform nest)
    {
        Animate();
        Finished?.Invoke(nest);        
    }
}
