using System.Collections.Generic;
using UnityEngine;

public class ColorChangeGate : MonoBehaviour
{
    [SerializeField] private EggType[] _eggTypes;

    public IReadOnlyCollection<EggType> EggsType => _eggTypes;
}
