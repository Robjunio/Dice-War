using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PawnController _player1;
    private PawnController _player2;

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayer1(PawnController player)
    {
        _player1 = player;
    }

    public void SetPlayer2(PawnController player)
    {
        _player2 = player;
    }

    public PawnController GetPlayer1()
    {
        return _player1;
    }

    public PawnController GetPlayer2()
    {
        return _player2;
    }
}

