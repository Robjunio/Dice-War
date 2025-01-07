using DG.Tweening;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private LayerMask player;
    
    private bool canMove = false;

    // Start is called before the first frame update
    public void InitializeMovement()
    {
        canMove = true;
    }

    public void StopMove()
    {
        canMove = false;
    }

    private void Update()
    {
        if (!canMove) return;

        if(Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layersToHit))
        {
            Vector3 boardPosition = hit.transform.position;
            if (Vector3.Distance(transform.position, boardPosition) < 0.4f)
            {
                Debug.Log("You already in this tile");
                return;
            }

            if (Vector3.Distance(transform.position, boardPosition) < 2f)
            {
                // Cast a raycast to check if the enemy is in that position
                RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

                Debug.Log(hits.Length);

                foreach(RaycastHit collision in hits)
                {
                    if (collision.transform.CompareTag("Player"))
                    {
                        Debug.Log("You cannot go to the same tile that your opponent are");
                        return;
                    }
                }

                transform.DOMove(boardPosition, 0.3f);
                EventsManager.Instance.OnPlayerMove();
                return;
            }

        }
    }
}
