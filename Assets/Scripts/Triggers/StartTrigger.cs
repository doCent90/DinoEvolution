using System;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public event Action Started;

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
