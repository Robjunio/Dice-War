using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{  
    [SerializeField] GameObject[] powerUpsPrefabs;
    List<GameObject> powerUps = new List<GameObject>();
    private int _powerUpTotal;
    private int _powerUpCounter = 0;

    private void StartPowerUps(Vector3 player1pos, Vector3 player2pos, MatchSettings matchSettings, BoardMatrix boardMatrix)
    {
        _powerUpTotal = matchSettings.rows * matchSettings.cols;

        for (int i = 0; i < _powerUpTotal; i++)
        {
            if (boardMatrix.GetSpawnPointPosition(i) != player1pos && boardMatrix.GetSpawnPointPosition(i) != player2pos)
            {
                var powerUp = GetRandomPowerUp(UpdatePositionForPowerUp(boardMatrix.GetSpawnPointPosition(i)));
                powerUps.Add(powerUp);
                powerUp.transform.SetParent(transform, true);

                _powerUpCounter++;
            }
        }

        EventsManager.Instance.OnPowerUpsInPosition(player1pos, player2pos, matchSettings, boardMatrix);
    }

    private Vector3 UpdatePositionForPowerUp(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y + 0.05f, pos.z);
    }

    private GameObject GetRandomPowerUp(Vector3 position)
    {
        var pos = Random.Range(0, powerUpsPrefabs.Length);
        return Instantiate(powerUpsPrefabs[pos], position, Quaternion.identity);
    }

    private void PowerUpCollected()
    {
        _powerUpCounter--;
        if(_powerUpTotal * 0.1f >= _powerUpCounter)
        {
            ResetPowerUps();
        }
    }

    private void ResetPowerUps()
    {
        _powerUpCounter = _powerUpTotal;
        foreach (var powerUp in powerUps)
        {
            powerUp.SetActive(true);
        }
    }

    private void OnEnable()
    {
        EventsManager.PlayerCollectAPowerUp += PowerUpCollected;
        EventsManager.PlayersInPosition += StartPowerUps;
    }

    private void OnDisable()
    {
        EventsManager.PlayerCollectAPowerUp -= PowerUpCollected;
        EventsManager.PlayersInPosition -= StartPowerUps;
    }
}
