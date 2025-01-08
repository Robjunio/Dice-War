using TMPro;
using UnityEngine;

public class WinnerScreen : MonoBehaviour
{
    [SerializeField] TMP_Text winner;

    private void ActivateWinnerScreen(string winnerName)
    {
        winner.text = winnerName;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        EventsManager.PlayerWin += ActivateWinnerScreen;
    }

    private void OnDisable()
    {
        EventsManager.PlayerWin -= ActivateWinnerScreen;
    }
}
