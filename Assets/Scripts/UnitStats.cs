using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IComparable
{
    [SerializeField] int health;
    [SerializeField] int mana;
    [SerializeField] int attack;
    [SerializeField] int magic;
    [SerializeField] int defense;
    [SerializeField] int speed;

    public int Speed { get => speed; set => speed = value; }

    void Start() 
    {
    }

    // descending order (i.e. put the greater before the lesser -- i.e. big should be negative compared to small)
    public int CompareTo(object other)
    {
        return  ((UnitStats) other).Speed - Speed;
    }

    public override String ToString() {
        return gameObject.name;
    }

    internal void Act(TurnSystem currentBattle)
    {
        print($"{gameObject.name} up to bat.");
        if (gameObject.tag == "EnemyUnit") {
            StartCoroutine("EnemyAction", currentBattle);
        }
    }

    IEnumerator EnemyAction(TurnSystem currentBattle) 
    {
        yield return new WaitForSeconds(2);
        currentBattle.NextTurn();
    }

}
