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
        int targetCode = Random.Range(0, playerUnits.Length);
        GameObject target = playerUnits[targetCode];
        yield return AttackTarget(currentBattle, target);
    }

    public IEnumerator AttackTarget(TurnSystem currentBattle, GameObject target)
    {
        waitForAnimations = true;
        int damage = Random.Range(1, ownerStats.Attack);
        animator.Play(attackAnimation);
        yield return new WaitUntil(() => waitForAnimations == false);
        yield return target.GetComponent<UnitStats>().BeAttacked(damage, currentBattle);
        currentBattle.NextTurn();
    }

    public void StopAwaitingAnimation() {
        waitForAnimations = false;
    }

}
