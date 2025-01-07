using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    private GameObject _tile;
    private BoardMatrix _boardMatrix;

    void Awake()
    {
        _tile = Resources.Load<GameObject>("Prefabs/Tile - Board");
        BoardSettings boardSettings = Resources.Load<BoardSettings>("ScriptableObjects/BoardSettings");
        if(boardSettings != null)
        {
            CreateBoard(boardSettings);
        }
    }

    void CreateBoard(BoardSettings boardSettings)
    {
        _boardMatrix = new BoardMatrix(boardSettings.cols, boardSettings.rows, transform.position);

        for (int i = 0; i < boardSettings.rows * boardSettings.cols; i++)
        {
            GameObject newTile = Instantiate(_tile, _boardMatrix.GetSpawnPointPosition(i), Quaternion.Euler(90, 0, 0));
            newTile.transform.SetParent(transform, true);
        }
    }
}
