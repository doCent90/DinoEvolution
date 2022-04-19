using UnityEngine;

public class RegDollKicker : MonoBehaviour
{
    [SerializeField] private bool _isBoss;

    private Rigidbody[] _rigidbodies;
    private readonly float _range = 6f;

    private void OnEnable()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void Kick()
    {
        foreach (var rigidbody in _rigidbodies)
            Move(rigidbody, _range);
    }

    private void Move(Rigidbody rigidbody, float range)
    {
        float x;
        float z;
        float y;

        x = Random.Range(-range, range) / 3;
        y = Random.Range(0, range);

        if(_isBoss == false)
            z = -range;
        else
            z = range;

        rigidbody.isKinematic = false;
        rigidbody.AddForce(new Vector3(x, y, z), ForceMode.VelocityChange);
    }
}
