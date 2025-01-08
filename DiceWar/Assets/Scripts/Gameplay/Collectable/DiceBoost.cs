using UnityEngine;

public class DiceBoost : Collectable
{
    [SerializeField] int _extraDices = 1;

    protected override void UpgradeCollectable()
    {
        base.UpgradeCollectable();

        _extraDices *= 2;
    }

    public override void Collect(PawnController player)
    {
        base.Collect(player);
        player.AddDices(_extraDices);

        AudioClip audioClip = Resources.Load<AudioClip>("Sounds/PowerUp_DiceBoost");
        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        gameObject.SetActive(false);

    }
}

