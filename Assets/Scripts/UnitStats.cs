using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IComparable
{
    // stats
    [SerializeField] int attack;
    public int Attack { get => attack; }
    [SerializeField] int speed;
    public int Speed { get => speed; set => speed = value; }

    // components
    Attack attackComponent;

    // animation
    Animator animator;
    [SerializeField] string hitAnimation;
    bool waitForAnimation;
    [SerializeField] GameObject damageDisplay;
    GameObject HUD;

    void Start() 
    {
        attackComponent = gameObject.GetComponent<Attack>();
        animator = gameObject.GetComponent<Animator>();
        HUD = GameObject.FindGameObjectWithTag("BattleHUD");
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
        yield return attackComponent.AttackTarget();
        currentBattle.NextTurn();
    }

    internal IEnumerator BeAttacked(int damage)
    {
        waitForAnimation = true;
        print($"{gameObject.name} attacked for {damage}");
        animator.Play(hitAnimation);
        GameObject hitDamageDisplay = Instantiate(damageDisplay, HUD.transform, false);
        yield return new WaitUntil(() => waitForAnimation == false);
    }

    public void StopAwaitingAnimation() {
        waitForAnimation = false;
    }
}
