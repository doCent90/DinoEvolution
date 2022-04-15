using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Egg", menuName = "Eggs/Create Egg", order = 51)]
public class EggType : ScriptableObject
{
    [Header("Level 1")]
    [SerializeField] private EggModel _eggModelTypeLevel1;
    [SerializeField] private int _damageLevel1;
    [SerializeField] private int _healthLevel1;
    [Header("Level 2")]
    [SerializeField] private EggModel _eggModelTypeLevel2;
    [SerializeField] private int _damageLevel2;
    [SerializeField] private int _healthLevel2;
    [Header("Level 3")]
    [SerializeField] private EggModel _eggModelTypeLevel3;
    [SerializeField] private int _damageLevel3;
    [SerializeField] private int _healthLevel3;
    [Header("Level 4")]
    [SerializeField] private EggModel _eggModelTypeLevel4;
    [SerializeField] private int _damageLevel4;
    [SerializeField] private int _healthLevel4;
    [Header("Level 5")]
    [SerializeField] private EggModel _eggModelTypeLevel5;
    [SerializeField] private int _damageLevel5;
    [SerializeField] private int _healthLevel5;

    public EggModel EggModelType { get; private set; }
    public float Damage { get; private set; }
    public int Health  { get; private set; }

    public EggType Init(EggLevel eggLevel)
    {
        switch ((int)eggLevel)
        {
            case 0:
                SetValues(_eggModelTypeLevel1, _damageLevel1, _healthLevel1);
                break;
            case 1:
                SetValues(_eggModelTypeLevel2, _damageLevel2, _healthLevel2);
                break;
            case 2:
                SetValues(_eggModelTypeLevel3, _damageLevel3, _healthLevel3);
                break;
            case 3:
                SetValues(_eggModelTypeLevel4, _damageLevel4, _healthLevel4);
                break;
            case 4:
                SetValues(_eggModelTypeLevel5, _damageLevel5, _healthLevel5);
                break;
            default:
                SetValues(_eggModelTypeLevel5, _damageLevel5, _healthLevel5);
                break;
        }
        return this;
    }

    private void SetValues(EggModel eggModel, int damage, int health)
    {
        EggModelType = eggModel;
        Damage = damage;
        Health = health;
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
