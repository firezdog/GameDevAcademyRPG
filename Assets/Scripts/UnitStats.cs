using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IComparable
{
    // stats
    [SerializeField] int health;
    [SerializeField] int attack;
    [SerializeField] int speed;

    // accessors
    public int Attack { get => attack; }
    public int Speed { get => speed; set => speed = value; }
    public int Health { get => health; set => health = value; }

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
        yield return attackComponent.AttackTarget(currentBattle);
        currentBattle.NextTurn();
    }

    internal IEnumerator BeAttacked(int damage, TurnSystem currentBattle)
    {
        waitForAnimation = true;
        print($"{gameObject.name} attacked for {damage}");
        animator.Play(hitAnimation);
        yield return new WaitUntil(() => waitForAnimation == false);
        var myDamageDisplay = Instantiate(
            damageDisplay, 
            gameObject.transform.position, 
            gameObject.transform.rotation, 
            HUD.transform
        );
        health = Math.Max(health - damage, 0);
        if (health == 0) {
            // kill player unit
            gameObject.tag = "DeadPlayerUnit";
            gameObject.SetActive(false);
            currentBattle.RemoveUnitFromBattle(this);
        }
        myDamageDisplay.GetComponentInChildren<DamageDisplay>().SetText(damage.ToString());
    }

    public void StopAwaitingAnimation() {
        waitForAnimation = false;
    }

}
