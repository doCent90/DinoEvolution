using System.Linq;
using UnityEngine;

public class EggsCounter : MonoBehaviour
{
    public int EggCount { get; private set; }

    private void OnEnable()
    {
        Egg[] eggs = GetComponentsInChildren<Egg>();

        EggCount = eggs.OrderBy(egg => egg.gameObject.activeSelf).Count();
    }
}
