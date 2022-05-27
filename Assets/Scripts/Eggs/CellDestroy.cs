using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CellDestroy : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private const float Delay = 5f;
    private const float RangeDelay = 0.1f;
    private const float Power = 60;

    public void Destroy(Transform parent)
    {
        if (gameObject.activeSelf == false)
            return;

        transform.parent = parent;

        if(_renderer != null)
            _renderer.enabled = true;

        float randomDelay = Random.Range(0, RangeDelay);
        Invoke(nameof(Move), randomDelay);
        StartCoroutine(TimeToDestroy());
    }

    private void OnEnable()
    {
        if(GetComponent<Renderer>())
            _renderer = GetComponent<Renderer>();

        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private IEnumerator TimeToDestroy()
    {
        var waitForSeconds = new WaitForSeconds(Delay);
        yield return waitForSeconds;        
        gameObject.SetActive(false);
    }

    private void Move()
    {
        float x = Random.Range(-Power, Power);
        float z = Random.Range(-Power / 2, -Power * 2);
        float y = Random.Range(-Power, Power);

        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Force);
    }
}
