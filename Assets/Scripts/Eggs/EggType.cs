using UnityEngine;

[CreateAssetMenu(fileName = "New Egg", menuName = "Eggs/Create Egg", order = 51)]
public class EggType : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] private EggLevel _eggLevel;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [Header("Don`t toch!")]
    [SerializeField] private EggModel[] _eggModelType;

    public float Damage => _damage;
    public int Health => _health;

    public EggModel Init()
    {
        return _eggModelType[(int)_eggLevel];
    }
}

public enum EggLevel
{
    Level_1,
    Level_2,
    Level_3,
    Level_4,
    Level_5
}
