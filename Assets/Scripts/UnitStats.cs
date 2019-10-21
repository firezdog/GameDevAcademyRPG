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
        if (HUD == null) HUD = GameObject.FindGameObjectWithTag("BattleHUD");
        if (gameObject.tag == "EnemyUnit") {
            StartCoroutine(EnemyAction(currentBattle));
        }
    }

    IEnumerator EnemyAction(TurnSystem currentBattle) 
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if (playerUnits.Length == 0) {
            yield return new WaitForSeconds(2);
            currentBattle.Lose();
            yield break;
        }
        yield return attackComponent.AttackTarget(currentBattle, playerUnits);
        currentBattle.NextTurn();
    }

    internal IEnumerator BeAttacked(int damage, TurnSystem currentBattle)
    {
        waitForAnimation = true;
        animator.Play(hitAnimation);
        yield return new WaitUntil(() => waitForAnimation == false);
        var myDamageDisplay = Instantiate(
            damageDisplay, 
            gameObject.transform.position, 
            gameObject.transform.rotation, 
            HUD.transform
        );
        myDamageDisplay.GetComponentInChildren<DamageDisplay>().SetText(damage.ToString());
        health = Math.Max(health - damage, 0);
        if (health == 0) {
            // kill player unit
            gameObject.tag = "DeadPlayerUnit";
            gameObject.SetActive(false);
            currentBattle.RemoveUnitFromBattle(this);
        }
    }

    public void StopAwaitingAnimation() {
        waitForAnimation = false;
    }

}
