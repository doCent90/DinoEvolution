using UnityEngine;

public class EggModel : MonoBehaviour
{
    private CellDestroy[] _cellDestroys;

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
