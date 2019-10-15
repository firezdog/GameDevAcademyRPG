using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] string attackAnimation;

    UnitStats ownerStats;
    Animator animator;

    void Start()
    {
        ownerStats = gameObject.GetComponent<UnitStats>();
        animator = gameObject.GetComponent<Animator>();
    }

    public IEnumerator AttackTarget() 
    {
        int attack = Random.Range(0, ownerStats.Attack);
        print(attack);
        gameObject.GetComponent<Animator>().Play(attackAnimation);
        yield return new WaitForSeconds(3);
        // yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation));
    }
}
