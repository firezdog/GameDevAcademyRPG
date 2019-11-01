using System.Collections;
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

    public IEnumerator AttackTarget(TurnSystem currentBattle, GameObject[] playerUnits) 
    {
        waitForAnimations = true;
        // might be a performance hit here
        int targetCode = Random.Range(0, playerUnits.Length);
        GameObject target = playerUnits[targetCode];
        int damage = Random.Range(0, ownerStats.Attack);
        animator.Play(attackAnimation);
        yield return new WaitUntil(() => waitForAnimations == false);
        yield return target.GetComponent<UnitStats>().BeAttacked(damage, currentBattle);
    }

    public void AttackTarget(TurnSystem currentBattle, GameObject target)
    {
        print ($"Attacking {target}");
    }

    public void StopAwaitingAnimation() {
        waitForAnimations = false;
    }

}
