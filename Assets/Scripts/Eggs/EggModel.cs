using UnityEngine;

public class EggModel : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _meshRenderers;

    private CellDestroy[] _cellDestroys;

    public void EnableCleanCells()
    {
        foreach (var cell in _meshRenderers)
            cell.enabled = true;
    }

    public void DestroyCells()
    {
        foreach (var cell in _cellDestroys)
            cell.Destroy();
    }

    private void OnEnable()
    {
        _cellDestroys = GetComponentsInChildren<CellDestroy>();
    }
}
