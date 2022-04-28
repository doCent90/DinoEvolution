using TMPro;
using UnityEngine;

public class TestLevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        _text.text = PlayerPrefs.GetInt("level").ToString();
    }
}
