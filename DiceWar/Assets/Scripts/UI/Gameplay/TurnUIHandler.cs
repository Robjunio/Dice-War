using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TurnUIHandler : MonoBehaviour
{
    [SerializeField] TMP_Text player1Turn;
    [SerializeField] TMP_Text player2Turn;

    private void Player1TurnTransition()
    {
        StartCoroutine(Player1Turn());
    }

    IEnumerator Player1Turn()
    {
        player1Turn.transform.localPosition = new Vector3(-2000f, 0f, 0f);
        yield return player1Turn.rectTransform.DOAnchorPos(new Vector2(0f, 0f), 0.4f, false).SetEase(Ease.OutQuart).WaitForCompletion();
        yield return player1Turn.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.8f).SetLoops(4, LoopType.Yoyo).WaitForCompletion();
        yield return player1Turn.rectTransform.DOAnchorPos(new Vector2(2000f, 0f), 0.4f, false).SetEase(Ease.OutQuart).WaitForCompletion();

    }

    private void Player2TurnTransition()
    {
        StartCoroutine(Player2Turn());
    }

    IEnumerator Player2Turn()
    {
        player2Turn.transform.localPosition = new Vector3(-2000f, 0f, 0f);
        yield return player2Turn.rectTransform.DOAnchorPos(new Vector2(0f, 0f), 0.4f, false).SetEase(Ease.OutQuart).WaitForCompletion();
        yield return player2Turn.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.8f).SetLoops(4, LoopType.Yoyo).WaitForCompletion();
        yield return player2Turn.rectTransform.DOAnchorPos(new Vector2(2000f, 0f), 0.4f, false).SetEase(Ease.OutQuart).WaitForCompletion();

    }

    private void OnEnable()
    {
        EventsManager.Player1Turn += Player1TurnTransition;
        EventsManager.Player2Turn += Player2TurnTransition;
    }

    private void OnDisable()
    {
        EventsManager.Player1Turn -= Player1TurnTransition;
        EventsManager .Player2Turn -= Player2TurnTransition;
    }
}
