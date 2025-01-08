using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] DiceChecker diceChecker;
    private PawnController attacker;
    private int attackerDiceCount;
    private List<int> attackerDiceList;

    private PawnController defender;
    private int defenderDiceCount;
    private List<int> defendersDiceList;

    private void PrepareForBattle()
    {
        if(GameManager.Instance.GetPlayer1().IsMyTurn())
        {
            attacker = GameManager.Instance.GetPlayer1();
            defender = GameManager.Instance.GetPlayer2();
        }
        else
        {
            attacker = GameManager.Instance.GetPlayer2();
            defender = GameManager.Instance.GetPlayer1();
        }

        attackerDiceCount = attacker.GetDiceQuantity();
        defenderDiceCount = defender.GetDiceQuantity();

        StartCoroutine(AttackersTurn(attacker.GetDice()));
    }

    private IEnumerator AttackersTurn(GameObject dicePrefab)
    {
        GameObject[] dices = new GameObject[attackerDiceCount];
        for(int i = 0; i < attackerDiceCount; i++)
        {
            var dice = Instantiate(dicePrefab, transform.position, Quaternion.identity);
            dice.GetComponent<DiceHandler>().Throw();

            dices[i] = dice;

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(5f);

        attackerDiceList = diceChecker.GetDicesValues();

        for (int i = 0; i < attackerDiceCount; i++)
        {
            Destroy(dices[i]);
        }

        StartCoroutine(DefendersTurn(defender.GetDice()));
    }
    private IEnumerator DefendersTurn(GameObject dicePrefab)
    {
        GameObject[] dices = new GameObject[defenderDiceCount];
        for (int i = 0; i < defenderDiceCount; i++)
        {
            var dice = Instantiate(dicePrefab, transform.position, Quaternion.identity);
            dice.GetComponent<DiceHandler>().Throw();

            dices[i] = dice;

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(5f);

        defendersDiceList = diceChecker.GetDicesValues();

        for (int i = 0; i < defenderDiceCount; i++)
        {
            Destroy(dices[i]);
        }

        BattleResult();
    }

    private void BattleResult()
    {
        int scoreAttacker = 0;
        int scoreDefender = 0;

        for (int i = 0; i < defenderDiceCount; i++)
        {
            if (attackerDiceList[i] >= defendersDiceList[i])
            {
                scoreAttacker++;
                Debug.Log("Attacker won fight - " + attackerDiceList[i] + " x " + defendersDiceList[i]);
            }
            else
            {
                scoreDefender++;
                Debug.Log("Defender won fight - " + attackerDiceList[i] + " x " + defendersDiceList[i]);
            }
        }
        
        if(scoreAttacker > scoreDefender)
        {
            defender.GetHit(attacker.GetAttackPower());
            Debug.Log("Attacker won! - Damage in defender: " + attacker.GetAttackPower());
        } 
        else
        {
            attacker.GetHit(defender.GetAttackPower());
            Debug.Log("Defender won! - Damage in attacker: " + defender.GetAttackPower());
        }

        attacker.CheckTurnAfterBattle();
    }

    private void OnEnable()
    {
        EventsManager.PlayerStartABattle += PrepareForBattle;
    }

    private void OnDisable()
    {
        EventsManager.PlayerStartABattle -= PrepareForBattle;
    }
}
