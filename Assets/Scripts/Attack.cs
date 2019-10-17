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

    public IEnumerator AttackTarget() 
    {
        waitForAnimations = true;
        // might be a performance hit here
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        int targetCode = Random.Range(0, playerUnits.Length);
        GameObject target = playerUnits[targetCode];
        int damage = Random.Range(0, ownerStats.Attack);
        print($"{gameObject.name} attacking {target.name} for {damage}");
        animator.Play(attackAnimation);
        yield return new WaitUntil(() => waitForAnimations == false);
        yield return target.GetComponent<UnitStats>().BeAttacked(damage);
    }

    public void StopAwaitingAnimation() {
        waitForAnimations = false;
    }

}
