using TMPro;
using UnityEngine;

public class LevelViewer : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _level;

    public void Show(int level)
    {
        _level.text = level.ToString();
    }
}
