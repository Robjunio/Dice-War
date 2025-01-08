using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    private void TargetPlayer1()
    {
        target = GameManager.Instance.GetPlayer1().transform;
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.DOMove(target.position, 0.4f);
    }

    IEnumerator BackToPlayer()
    {
        yield return new WaitForSeconds(13f);
        FollowTarget();
    }

    private void TargetPlayer2()
    {
        target = GameManager.Instance.GetPlayer2().transform;
        FollowTarget();
    }

    private void AimToMiddle()
    {
        transform.DOMove(Vector3.zero, 0.4f);
        StartCoroutine(BackToPlayer());
    }

    private void OnEnable()
    {
        EventsManager.Player1Turn += TargetPlayer1;
        EventsManager.Player2Turn += TargetPlayer2;
        EventsManager.PlayerMove += FollowTarget;
        EventsManager.PlayerStartABattle += AimToMiddle;
    }

    private void OnDisable()
    {
        EventsManager.Player1Turn -= TargetPlayer1;
        EventsManager.Player2Turn -= TargetPlayer2;
        EventsManager.PlayerMove -= FollowTarget;
        EventsManager.PlayerStartABattle -= AimToMiddle;
    }
}
