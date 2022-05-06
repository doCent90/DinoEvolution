using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EggModel : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _meshRenderers;

    private RoadParent _roadParent;
    private CellDestroy[] _cellDestroys;

    private const float Multiply = 1.1f;

    private void OnEnable()
    {
        _roadParent = FindObjectOfType<RoadParent>();
        _cellDestroys = GetComponentsInChildren<CellDestroy>();
    }

    public void IncreaseScale()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * Multiply;
    }

    public void EnableCleanCells()
    {
        foreach (MeshRenderer cell in _meshRenderers)
            cell.enabled = true;
    }

    public void DestroyCells()
    {
        foreach (CellDestroy cell in _cellDestroys)
            cell.Destroy(_roadParent.transform);
    }
}
