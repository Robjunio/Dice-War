using UnityEngine;

public class Healing : Collectable
{
    [SerializeField] int _heal = 1;


    protected override void UpgradeCollectable()
    {
        base.UpgradeCollectable();

        _heal *= 2;
    }

    public override void Collect(PawnController player)
    {
        base.Collect(player);
        player.Heal(_heal);
        
        gameObject.SetActive(false);
    }
}
