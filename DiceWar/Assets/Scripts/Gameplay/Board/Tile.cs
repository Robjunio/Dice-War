using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isActive = false;
    
    public void ActivateTile()
    {
        isActive = true;
        transform.DOMoveY(0.1f, 0.1f);
    }
    public void DeactivateTile() {
        transform.DOMoveY(-0.1f, 0.05f);    
        isActive = false;
    }

    private void OnEnable()
    {
        EventsManager.PlayerMove += DeactivateTile;
    }

    private void OnDisable()
    {
        EventsManager.PlayerMove -= DeactivateTile;
    }

}
