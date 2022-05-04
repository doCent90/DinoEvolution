using System;
using UnityEngine;

public class Dinosaur : MonoBehaviour
{
    public float Health { get; protected set; }
    public float Damage { get; protected set; }

    public event Action Kicked;

    protected void OnDied()
    {
        Kicked?.Invoke();
    }
}
