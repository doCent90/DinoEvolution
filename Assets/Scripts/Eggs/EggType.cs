using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Egg", menuName = "Eggs/Create Egg", order = 51)]
public class EggType : ScriptableObject
{
    [Header("Level 1")]
    [SerializeField] private EggData _level1;
    [Header("Level 2")]
    [SerializeField] private EggData _level2;
    [Header("Level 3")]
    [SerializeField] private EggData _level3;
    [Header("Level 4")]
    [SerializeField] private EggData _level4;
    [Header("Level 5")]
    [SerializeField] private EggData _level5;

    public EggData GetTypeData(EggLevel eggLevel)
    {
        switch ((int)eggLevel)
        {
            case 0:
                return _level1;
            case 1:
                return _level2;
            case 2:
                return _level3;
            case 3:
                return _level4;
            case 4:
                return _level5;
            default:
                return _level5;
        }
    }
}

[Serializable]
public class EggData
{
    [SerializeField] private EggModel _eggModelType;
    [SerializeField] private Dino _dino;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;

    public EggModel EggModelType => _eggModelType;
    public Dino Dino => _dino;
    public int Damage => _damage;
    public int Health => _health;
}

public enum EggLevel
{
    Level_1,
    Level_2,
    Level_3,
    Level_4,
    Level_5
}
