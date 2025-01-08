using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public delegate void PrepareTable(Vector3 player1Spawn, Vector3 player2Spawn, MatchSettings matchSettings, BoardMatrix boardMatrix);
    public delegate void StartBattle();
    public static event PrepareTable BoardPrepared;
    public static event PrepareTable PlayersInPosition;
    public static event PrepareTable PowerUpsInPosition;
    public static event StartBattle Player1Turn;
    public static event StartBattle Player2Turn;
    public static event StartBattle PlayerMove;
    public static event StartBattle PlayerCollectAPowerUp;

    public static EventsManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void OnBoardReady(Vector3 player1Spawn, Vector3 player2Spawn, MatchSettings matchSettings, BoardMatrix boardMatrix)
    {
        BoardPrepared?.Invoke(player1Spawn, player2Spawn, matchSettings, boardMatrix);
    }

    public void OnPlayersInPosition(Vector3 player1Spawn, Vector3 player2Spawn, MatchSettings matchSettings, BoardMatrix boardMatrix)
    {
        PlayersInPosition?.Invoke(player1Spawn, player2Spawn, matchSettings, boardMatrix);
    }
    public void OnPowerUpsInPosition(Vector3 player1Spawn, Vector3 player2Spawn, MatchSettings matchSettings, BoardMatrix boardMatrix)
    {
        PowerUpsInPosition?.Invoke(player1Spawn, player2Spawn, matchSettings, boardMatrix);

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

    public void OnPlayerCollectedAPowerUp()
    {
        PlayerCollectAPowerUp?.Invoke();
    }
}
