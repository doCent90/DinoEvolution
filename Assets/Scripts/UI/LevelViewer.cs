using TMPro;
using UnityEngine;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private LevelsLoader _levelsLoader;
    [Header("Text")]
    [SerializeField] private TMP_Text _level;

    private void OnEnable()
    {
        _levelsLoader.LevelLoaded += Show;
    }

    private void OnDisable()
    {
        _levelsLoader.LevelLoaded -= Show;        
    }

    private void Show(int level)
    {
        _level.text = level.ToString();
    }
}
