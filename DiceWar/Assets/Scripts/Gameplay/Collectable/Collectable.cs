using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnEnable()
    {
        CheckUpgrade();
    }

    protected void CheckUpgrade()
    {
        int probability = Mathf.FloorToInt(Random.Range(0, 101));

        if(probability > 70) {
            UpgradeCollectable();
        }
    }

    protected virtual void UpgradeCollectable()
    {
        transform.localScale = transform.localScale * 1.1f;
    }

    public virtual void Collect(PawnController player)
    {
        EventsManager.Instance.OnPlayerCollectedAPowerUp();
    }

}
