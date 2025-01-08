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
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, layersToHit);
        if (hits.Length > 0)
        {
            Vector3 boardPosition = hits[0].transform.position;
            if (Vector3.Distance(transform.position, boardPosition) < 0.4f)
            {
                Debug.Log("You already in this tile");
                return;
            }

            if (Vector3.Distance(transform.position, boardPosition) < 1.4f)
            {
                foreach(RaycastHit hit in hits)
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        Debug.Log("You cannot go to the same tile that your opponent are");
                        return;
                    }
                }

                canMove = false;

                transform.DOMove(boardPosition, 0.3f).OnComplete(() => {
                    AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Move");
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                    EventsManager.Instance.OnPlayerMove();
                });
                return;
            }

        }
    }
}
