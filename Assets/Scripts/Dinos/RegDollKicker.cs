using UnityEngine;

public class RegDollKicker : MonoBehaviour
{
    [SerializeField] private Dinosaur _dinosaur;

    private Rigidbody[] _rigidbodies;
    private readonly float _range = 7f;

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
        foreach (Rigidbody rigidbody in _rigidbodies)
            Move(rigidbody, _range);
    }

    private void Move(Rigidbody rigidbody, float range)
    {
        float x = Random.Range(-range, range) / 3;
        float y = range / 2;
        float z = range;

        rigidbody.isKinematic = false;

        if (_dinosaur is DinoMini)
            rigidbody.AddForce(new Vector3(x, y, -z), ForceMode.VelocityChange);
        else
            rigidbody.AddForce(new Vector3(0, y, z / 3.5f), ForceMode.VelocityChange);
    }
}
