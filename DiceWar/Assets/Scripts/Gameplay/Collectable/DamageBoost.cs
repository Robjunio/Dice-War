using UnityEngine;

public class DamageBoost : Collectable
{
    [SerializeField] int _extraDamage = 1;


    protected override void UpgradeCollectable()
    {
        base.UpgradeCollectable();

        _extraDamage *= 2;
    }

    public override void Collect(PawnController player)
    {
        base.Collect(player);
        player.AddAttack(_extraDamage);
        
        gameObject.SetActive(false);
    }
}
