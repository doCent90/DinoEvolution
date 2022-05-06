using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EggModel : MonoBehaviour
{
    [SerializeField] private Material _dirtyMaterial;
    [SerializeField] private MeshRenderer[] _meshRenderers;

    private RoadParent _roadParent;
    private CellDestroy[] _cellDestroys;
    private Material _originalMaterial;

    private const float Multiply = 1.1f;

    public MeshRenderer MainEgg { get; private set; }

    private void OnEnable()
    {
        MainEgg = GetComponent<MeshRenderer>();
        _roadParent = FindObjectOfType<RoadParent>();
        _cellDestroys = GetComponentsInChildren<CellDestroy>();

        _originalMaterial = MainEgg.material;
        SetDirtyMaterial();
    }

    public void SetOriginalMaterial()
    {
        MainEgg.material = _originalMaterial;
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

    private void SetDirtyMaterial()
    {
        MainEgg.material = _dirtyMaterial;
    }
}
