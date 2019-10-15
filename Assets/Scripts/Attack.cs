using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] string attackAnimation;

    UnitStats ownerStats;
    Animator animator;

    bool test;

    void Start()
    {
        ownerStats = gameObject.GetComponent<UnitStats>();
        animator = gameObject.GetComponent<Animator>();
    }

    public IEnumerator AttackTarget() 
    {
        test = false;
        int attack = Random.Range(0, ownerStats.Attack);
        print(attack);
        gameObject.GetComponent<Animator>().Play(attackAnimation);
        yield return new WaitUntil(() => test == true);
    }

    public void SetTest() {
        test = true;
    }

}
