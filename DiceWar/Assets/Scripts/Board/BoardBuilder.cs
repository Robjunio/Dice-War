using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    private GameObject _tile;
    private BoardMatrix _boardMatrix;
    
    private Vector3 _positionPlayer1Spawner;
    private Vector3 _positionPlayer2Spawner;

    void Awake()
    {
        _tile = Resources.Load<GameObject>("Prefabs/Tile - Board");
        MatchSettings boardSettings = Resources.Load<MatchSettings>("ScriptableObjects/BoardSettings");
        if(boardSettings != null)
        {
            CreateBoard(boardSettings);
        }
    }

    void CreateBoard(MatchSettings boardSettings)
    {
        int mapSize = boardSettings.rows * boardSettings.cols;
        _boardMatrix = new BoardMatrix(boardSettings.cols, boardSettings.rows, transform.position);

        int player1pos = Mathf.FloorToInt(Random.Range(0, mapSize));
        int player2pos = Mathf.FloorToInt(Random.Range(0, mapSize));

        if(player1pos == player2pos) {
            player1pos = 0;
            player2pos = mapSize - 1;
        }

        for (int i = 0; i < mapSize; i++)
        {
            GameObject newTile = Instantiate(_tile, _boardMatrix.GetSpawnPointPosition(i), Quaternion.Euler(90, 0, 0));
            newTile.transform.SetParent(transform, true);

            if(i == player1pos)
            {
                _positionPlayer1Spawner = _boardMatrix.GetSpawnPointPosition(i);
                newTile.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/BoardTile_Spawn");
            }

            if(i == player2pos)
            {
                _positionPlayer2Spawner = _boardMatrix.GetSpawnPointPosition(i);
                newTile.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/BoardTile_Spawn");
            }
        }

        EventsManager.Instance.OnBoardReady(_positionPlayer1Spawner, _positionPlayer2Spawner, boardSettings);

    }
}
