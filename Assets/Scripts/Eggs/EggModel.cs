using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EggModel : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRendererDino;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private CellDestroy[] _cellsPreOpenEgg; 

    private RoadParent _roadParent;
    private CellDestroy[] _cellDestroys;

    private const float Multiply = 1.1f;

    private void OnEnable()
    {
        _cellDestroys = GetComponentsInChildren<CellDestroy>();
    }

    public void Init(RoadParent roadParent)
    {
        _roadParent = roadParent;
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

        foreach (CellDestroy cell in _cellsPreOpenEgg)
            cell.Destroy(_roadParent.transform);

        if(_meshRendererDino != null)
            _meshRendererDino.enabled = true;
    }

    public void DestroyCells()
    {
        foreach (CellDestroy cell in _cellDestroys)
            cell.Destroy(_roadParent.transform);

        if(_meshRendererDino != null)
            _meshRendererDino.enabled = false;
    }
}
