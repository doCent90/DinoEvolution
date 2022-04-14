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

    private const float DELAY = 4f;
    private const float POWER = 10f;

    public void Destroy(Transform parent)
    {
        transform.parent = parent;

        if(_renderer != null)
            _renderer.enabled = true;

        Move(POWER);
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
        var waitForSeconds = new WaitForSeconds(DELAY);
        yield return waitForSeconds;        
        transform.DOScale(0.2f, DELAY / 2);
        yield return waitForSeconds;        
        gameObject.SetActive(false);
    }

    private void Move(float range)
    {
        float x;
        float z;
        float y;

        x = Random.Range(-range, range);
        z = Random.Range(range, range);
        y = Random.Range(0, range);

        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Force);
    }
}
