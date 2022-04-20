using UnityEngine;

public class RegDollKicker : MonoBehaviour
{
    [SerializeField] private Dinosaur _dinosaur;
    [SerializeField] private bool _isBoss;

    private Rigidbody[] _rigidbodies;
    private readonly float _range = 6f;

    private void OnEnable()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody dino in _rigidbodies)
            dino.isKinematic = true;

        _dinosaur.Kicked += Kick;
    }

    private void OnDisable()
    {
        _dinosaur.Kicked -= Kick;        
    }

    private void Kick()
    {
        foreach (var rigidbody in _rigidbodies)
            Move(rigidbody, _range);
    }

    private void Move(Rigidbody rigidbody, float range)
    {
        float x = Random.Range(-range, range) / 2;
        float y = Random.Range(0, range);
        float z;

        if(_dinosaur is Boss)
            z = range;
        else
            z = -range;

        rigidbody.isKinematic = false;
        rigidbody.AddForce(new Vector3(x, y, z), ForceMode.VelocityChange);
    }
}
