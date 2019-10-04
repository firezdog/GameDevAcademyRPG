using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnSystem : MonoBehaviour
{

    List<UnitStats> contestants;

    // Start is called before the first frame update
    void Start()
    {
        GetAllContestants();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllContestants()
    {
        contestants = new List<UnitStats>();
        GameObject[][] units = new GameObject[2][];
        units[0] = GameObject.FindGameObjectsWithTag("PlayerUnit");
        units[1] = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach(GameObject[] unitGroup in units)
        {
            foreach(GameObject unit in unitGroup)
            {
                UnitStats unitStats = unit.GetComponent<UnitStats>();
                unitStats.SetOrder();
                contestants.Add(unit.GetComponent<UnitStats>());
            }
        }
    }

    // get the next contestant to move
    void NextTurn()
    {
    }

}
