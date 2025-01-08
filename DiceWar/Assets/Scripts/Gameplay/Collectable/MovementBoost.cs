using UnityEngine;

public class MovementBoost : Collectable
{
    [SerializeField] int _extraMov = 1;

    protected override void UpgradeCollectable()
    {
        base.UpgradeCollectable();

        _extraMov *= 2;
    }

    public override void Collect(PawnController player)
    {
        base.Collect(player);
        player.AddMovement(_extraMov);

        AudioClip audioClip = Resources.Load<AudioClip>("Sounds/PowerUp_MovementBoost");
        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        gameObject.SetActive(false);
    }
}

