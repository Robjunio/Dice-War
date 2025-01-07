using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public delegate void PrepareTable(Vector3 player1Spawn, Vector3 player2Spawn, MatchSettings matchSettings);
    public delegate void StartBattle();
    public static event PrepareTable BoardPrepared;
    public static event PrepareTable PlayersInPosition;
    public static event StartBattle Player1Turn;
    public static event StartBattle Player2Turn;
    public static event StartBattle PlayerMove;

    public static EventsManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void OnBoardReady(Vector3 player1Spawn, Vector3 player2Spawn, MatchSettings matchSettings)
    {
        BoardPrepared?.Invoke(player1Spawn, player2Spawn, matchSettings);
    }

    public void OnPlayersInPosition(Vector3 player1Spawn, Vector3 player2Spawn, MatchSettings matchSettings)
    {
        PlayersInPosition?.Invoke(player1Spawn, player2Spawn, matchSettings);

        OnPlayer1Turn();
    }
    public void OnPlayer1Turn()
    {
        Player1Turn?.Invoke();
    }

    public void OnPlayer2Turn()
    {
        Player2Turn?.Invoke();
    }

    public void OnPlayerMove()
    {
        PlayerMove?.Invoke();
    }
}
