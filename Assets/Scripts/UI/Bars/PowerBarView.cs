using UnityEngine;
using UnityEngine.UI;

public class PowerBarView : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private Player _player;
    private EggsCounter _eggsCounter;

    private void OnDisable()
    {
        _player.EggCountChanged -= SetFill;
    }

    public void Init(Player player, EggsCounter eggsCounter)
    {
        _player = player;
        _eggsCounter = eggsCounter;
        _fill.fillAmount = 0;
        _player.EggCountChanged += SetFill;
    }

    private void SetFill(int count)
    {
        float maxFill = 1;
        float cellFill = maxFill;

        cellFill /= _eggsCounter.EggCount;
        _fill.fillAmount = cellFill * ((_eggsCounter.EggCount - count) - _eggsCounter.EggCount) * -1;
    }
}
