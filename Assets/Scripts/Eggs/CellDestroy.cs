using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(MeshCollider))]
public class CellDestroy : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private MeshCollider _collider;

    private const float Delay = 2f;
    private const float Power = 100f;

    public void Destroy(Transform parent)
    {
        transform.parent = parent;

        if(_renderer != null)
            _renderer.enabled = true;

        Move(Power);
        StartCoroutine(TimeToDestroy());
    }

    private void OnEnable()
    {
        if(GetComponent<Renderer>())
            _renderer = GetComponent<Renderer>();

        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<MeshCollider>();
    }

    private IEnumerator TimeToDestroy()
    {
        var waitForSeconds = new WaitForSeconds(Delay);
        yield return waitForSeconds;        
        gameObject.SetActive(false);
    }

    private void Move(float range)
    {
        float x;
        float z;
        float y;

        x = Random.Range(-range, range);
        y = Random.Range(0, range);
        z = 0;

        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Force);
    }
}
