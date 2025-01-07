using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    private int _moves = 3;
    private int _currentMovementsCounter = 0;
    private bool _myTurn = false;
    private bool _isPlayer1 = false;

    [SerializeField] private PawnMovement _pawnMovement;

    private void Awake()
    {
        var objects = FindObjectsOfType<PawnController>();
        if(objects.Length > 1)
        {
            _isPlayer1 = false;

            EventsManager.Player2Turn += StartTurn;
        }
        else
        {
            _isPlayer1 = true;
            EventsManager.Player1Turn += StartTurn;
        }
    }

    private void CheckTurn()
    {
        if (_myTurn)
        {
            _currentMovementsCounter--;
            if(_currentMovementsCounter == 0)
            {
                EndTurn();
            }
        }
    }
    private void EndTurn()
    {
        _pawnMovement.StopMove();
        _myTurn = false;

        // Delay for change turns
        transform.DOScale(transform.localScale, 0.2f).OnComplete(() =>
        {
            if (_isPlayer1)
            {
                EventsManager.Instance.OnPlayer2Turn();
            }
            else
            {
                EventsManager.Instance.OnPlayer1Turn();
            }
        }); 
    }

    private void StartTurn()
    {
        _myTurn = true;
        _currentMovementsCounter = _moves;
        _pawnMovement.InitializeMovement();
    }

    private void OnEnable()
    {
        EventsManager.PlayerMove += CheckTurn;
    }

    private void OnDisable()
    {
        if (_isPlayer1)
        {
            EventsManager.Player1Turn -= StartTurn;
        }
        else
        {
            EventsManager.Player2Turn -= StartTurn;
        }
    }
}
