using TMPro;
using UnityEngine;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private LevelsLoader _levelsLoader;
    [Header("Text")]
    [SerializeField] private TMP_Text _level;

    public void DisableText()
    {
        _level.enabled = false;
    }

    private void OnEnable()
    {
        _level.text = _levelsLoader.Level.ToString();
    }
}
