using UnityEngine;

public class DinosaurAttackHandler : MonoBehaviour
{
    [SerializeField] private Dinosaur _dino;

    private void Attack()  // Invoke from animation clip.
    {
        if(_dino is DinoMini dino)
            dino.Attack();

        if(_dino is Boss boss)
            boss.Attack();
    }
}
