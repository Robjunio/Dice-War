using DG.Tweening;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    [SerializeField] private int health = 10;
    private int _currentHealth;

    [SerializeField] private int attack = 1;

    private int _attackBonus = 0;

    private int _moves = 3;
    private int _currentMovementsCounter = 0;

    private int _dices = 3;
    private int _extraDices = 0;

    private bool _myTurn = false;
    private bool _isPlayer1 = false;

    [SerializeField] private PawnMovement _pawnMovement;
    [SerializeField] private PawnAttackSystem _pawnAttack;

    private GameObject _dice;

    private void Awake()
    {
        _currentHealth = health;
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

    private void Start()
    {
        if(_isPlayer1)
        {
            GameManager.Instance.SetPlayer1(this);
            _dice = Resources.Load<GameObject>("Prefabs/Dice_Purple");
        }
        else
        {
            GameManager.Instance.SetPlayer2(this);
            _dice = Resources.Load<GameObject>("Prefabs/Dice_Orange");
        }
    }
    public void CheckTurnAfterBattle()
    {
        if (_myTurn)
        {
            if (_currentMovementsCounter == 0)
            {
                EndTurn();
            }
            else
            {
                _pawnMovement.InitializeMovement();
            }
        }
    }
    private void CheckTurn()
    {
        if (_myTurn)
        {
            _currentMovementsCounter--;
            if (_currentMovementsCounter == 0)
            {
                if (!_pawnAttack.CheckAttackArea())
                {
                    EndTurn();
                }
                else
                {
                    _pawnMovement.StopMove();
                }
            }
            else
            {
                if (!_pawnAttack.CheckAttackArea())
                {
                    _pawnMovement.InitializeMovement();
                }
                else
                {
                    _pawnMovement.StopMove();
                }
            }
        }
    }
    private void EndTurn()
    {
        _pawnMovement.StopMove();
        _myTurn = false;

        _extraDices = 0;
        _attackBonus = 0;

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

        if (!_pawnAttack.CheckAttackArea()) {

            _pawnMovement.InitializeMovement();
        }
        else
        {
            _pawnMovement.StopMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.GetComponent<Collectable>().Collect(this);
        }
    }

    public void AddMovement(int value)
    {
        _currentMovementsCounter += value;
    }

    public void AddAttack(int value)
    {
        _attackBonus += value;
    }

    public void AddDices(int value)
    {
        _extraDices += value;
    }

    public void Heal(int value)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + value, 0, health);
    }

    public bool IsMyTurn()
    {
        return _myTurn;
    }

    public int GetDiceQuantity()
    {
        return _dices + _extraDices;
    }

    public int GetAttackPower()
    {
        return attack + _attackBonus;
    }

    public GameObject GetDice()
    {
        return _dice;
    }

    public void GetHit(int value)
    {
        _currentHealth -= value;

        AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Hit");
        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        if ( _currentHealth <= 0 )
        {
            if (_isPlayer1)
            {
                EventsManager.Instance.OnPlayerWin("Player 2");
            }
            else
            {
                EventsManager.Instance.OnPlayerWin("Player 1");
            }
            gameObject.SetActive(false);
        }
        else
        {
            // Hit animation
            transform.DOPunchScale(Vector3.one * 1.4f, 0.6f);
        }
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
