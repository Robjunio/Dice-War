using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void StartPlayers(Vector3 player1pos, Vector3 player2pos, MatchSettings matchSettings, BoardMatrix boardMatrix)
    {
        GameObject pawnPrefab = Resources.Load<GameObject>("Prefabs/Pawn");
        Material materialPlayer1 = Resources.Load<Material>("Materials/Board_Elements/Player_1");
        Material materialPlayer2 = Resources.Load<Material>("Materials/Board_Elements/Player_2");

        var player1 = Instantiate(pawnPrefab, player1pos, Quaternion.Euler(-90, 0, 0));
        player1.GetComponent<Renderer>().material = materialPlayer1;

        var player2 = Instantiate(pawnPrefab, player2pos, Quaternion.Euler(-90, 0, 0));
        player2.GetComponent<Renderer>().material = materialPlayer2;

        EventsManager.Instance.OnPlayersInPosition(player1pos, player2pos, matchSettings, boardMatrix);
    }

    private void OnEnable()
    {
        EventsManager.BoardPrepared += StartPlayers;
    }

    private void OnDisable()
    {
        EventsManager.BoardPrepared -= StartPlayers;
    }
}
