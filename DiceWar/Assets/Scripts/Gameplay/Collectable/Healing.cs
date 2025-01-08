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

        AudioClip audioClip = Resources.Load<AudioClip>("Sounds/PowerUp_Healing");
        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        gameObject.SetActive(false);
    }
}
