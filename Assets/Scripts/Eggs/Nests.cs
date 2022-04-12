using UnityEngine;

public class Nests : MonoBehaviour
{
    private Nest[] _nests;

    public void TakeEgg(Egg egg)
    {
        foreach (var nest in _nests)
        {
            if(nest.IsBusy == false)
            {
                Transform point = nest.GetNestPosition();
                egg.MoveToNest(point);
                continue;
            }
        }
    }

    private void OnEnable()
    {
        _nests = GetComponentsInChildren<Nest>();
    }
}
