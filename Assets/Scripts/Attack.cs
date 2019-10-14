using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    UnitStats ownerStats;

    void Start()
    {
        ownerStats = gameObject.GetComponent<UnitStats>();
    }

    public void AttackTarget() 
    {
        print(ownerStats.Attack);
    }
}
