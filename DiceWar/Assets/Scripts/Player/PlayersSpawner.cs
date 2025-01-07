using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void StartPlayers(Vector3 player1pos, Vector3 player2pos, MatchSettings matchSettings)
    {
        GameObject pawnPrefab = Resources.Load<GameObject>("Prefabs/Pawn");

        Instantiate(pawnPrefab, player1pos, Quaternion.Euler(-90, 0, 0));

        Instantiate(pawnPrefab, player2pos, Quaternion.Euler(-90, 0, 0));

        EventsManager.Instance.OnPlayersInPosition(player1pos, player2pos, matchSettings);
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
