using UnityEngine;

public class EggModel : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _meshRenderers;

    private RoadParent _roadParent;
    private CellDestroy[] _cellDestroys;

    public void EnableCleanCells()
    {
        foreach (var cell in _meshRenderers)
            cell.enabled = true;
    }

    public void DestroyCells()
    {
        foreach (var cell in _cellDestroys)
            cell.Destroy(_roadParent.transform);
    }

    private void OnEnable()
    {
        _roadParent = FindObjectOfType<RoadParent>();
        _cellDestroys = GetComponentsInChildren<CellDestroy>();
    }
}
