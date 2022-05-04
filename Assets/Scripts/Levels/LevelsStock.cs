using IJunior.TypedScenes;
using UnityEngine;

public static class LevelsStock
{
    private const int LevelsAmount = 15;
    private const int MinRandomLevel = 3;

    public static void LoadLevel(int number)
    {
        switch (number)
        {
            case 1:
                LVL_1.Load(number);
                break;
            case 2:
                LVL_2.Load(number);
                break;
            case 3:
                LVL_3.Load(number);
                break;
            case 4:
                LVL_4.Load(number);
                break;
            case 5:
                LVL_5.Load(number);
                break;
            case 6:
                LVL_6.Load(number);
                break;
            case 7:
                LVL_7.Load(number);
                break;
            case 8:
                LVL_8.Load(number);
                break;
            case 9:
                LVL_9.Load(number);
                break;
            case 10:
                LVL_10.Load(number);
                break;
            case 11:
                LVL_11.Load(number);
                break;
            case 12:
                LVL_12.Load(number);
                break;
            case 13:
                LVL_13.Load(number);
                break;
            case 14:
                LVL_14.Load(number);
                break;
            case 15:
                LVL_15.Load(number);
                break;
            default:
                SetRandomLevel();
                break;
        }
    }

    private static void SetRandomLevel()
    {
        int randomLevel = Random.Range(MinRandomLevel, LevelsAmount + 1);
        LoadLevel(randomLevel);
    }
}
