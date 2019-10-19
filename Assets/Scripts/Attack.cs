using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // components
    UnitStats ownerStats;

    // animations
    [SerializeField] string attackAnimation;
    Animator animator;
    public bool waitForAnimations;

    void Start()
    {
        ownerStats = gameObject.GetComponent<UnitStats>();
        animator = gameObject.GetComponent<Animator>();
    }

    public IEnumerator AttackTarget(TurnSystem currentBattle) 
    {
        waitForAnimations = true;
        // might be a performance hit here
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if (playerUnits.Length == 0) {
            print("No players left in battle!");
            yield break;
        }
        int targetCode = Random.Range(0, playerUnits.Length);
        GameObject target = playerUnits[targetCode];
        int damage = Random.Range(0, ownerStats.Attack);
        print($"{gameObject.name} attacking {target.name} for {damage}");
        animator.Play(attackAnimation);
        yield return new WaitUntil(() => waitForAnimations == false);
        yield return target.GetComponent<UnitStats>().BeAttacked(damage, currentBattle);
    }

    public void StopAwaitingAnimation() {
        waitForAnimations = false;
    }

}
