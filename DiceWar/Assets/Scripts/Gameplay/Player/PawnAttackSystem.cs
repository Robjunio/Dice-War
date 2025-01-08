using UnityEngine;

public class PawnAttackSystem : MonoBehaviour
{
    [SerializeField] LayerMask layerToHit;
    bool canAttack = true;
    
    public bool CheckAttackArea()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 20, Quaternion.identity, layerToHit);
        // Greater than 1 because it always check itself
        if(hitColliders.Length > 1 && canAttack)
        {
            canAttack = false;
            EventsManager.Instance.OnPlayerStartABattle();
            return true;
        }
        return false;
    }

    private void UnlockAttack()
    {
        canAttack = true;
    }

    private void OnEnable()
    {
        EventsManager.PlayerMove += UnlockAttack;
    }

    private void OnDisable()
    {
        EventsManager.PlayerMove -= UnlockAttack;
    }
}
