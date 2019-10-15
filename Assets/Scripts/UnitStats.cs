using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IComparable
{
    [SerializeField] int attack;
    public int Attack { get => attack; }
    [SerializeField] int speed;
    public int Speed { get => speed; set => speed = value; }

    Attack attacker;

    void Start() 
    {
        attacker = gameObject.GetComponent<Attack>();
    }

    // descending order (i.e. put the greater before the lesser -- i.e. big should be negative compared to small)
    public int CompareTo(object other)
    {
        return  ((UnitStats) other).Speed - Speed;
    }

    public override String ToString() {
        return gameObject.name;
    }

    // use currentBattle to access the NextTurn method when done acting.
    internal void Act(TurnSystem currentBattle)
    {
        print($"{gameObject.name} up to bat.");
        if (gameObject.tag == "EnemyUnit") {
            StartCoroutine(EnemyAction(currentBattle));
        }
    }

    IEnumerator EnemyAction(TurnSystem currentBattle) 
    {
        yield return attacker.AttackTarget();
        currentBattle.NextTurn();
    }

}
