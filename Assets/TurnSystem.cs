using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnSystem : MonoBehaviour
{

    List<UnitStats> turnQueue;
    List<UnitStats> nextQueue;
    bool waitForInput;

    // Start is called before the first frame update
    void Start()
    {
        GetAllContestants();
        NextTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && waitForInput) {
            waitForInput = false;
            NextTurn();
        }
    }

    void GetAllContestants()
    {
        turnQueue = new List<UnitStats>();
        nextQueue = new List<UnitStats>();
        GameObject[][] units = new GameObject[2][];
        units[0] = GameObject.FindGameObjectsWithTag("PlayerUnit");
        units[1] = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach(GameObject[] unitGroup in units)
        {
            foreach(GameObject unit in unitGroup)
            {
                UnitStats unitStats = unit.GetComponent<UnitStats>();
                turnQueue.Add(unit.GetComponent<UnitStats>());
            }
        }
        turnQueue.Sort();
    }

    // get the next contestant to move
    void NextTurn()
    {
        print(this);
        if (turnQueue.Count == 0 && nextQueue.Count == 0) { 
            Debug.Log("Error: No units in battle!");
            return;
        } else if (turnQueue.Count == 0) {
            // start next round
            turnQueue = nextQueue;
            turnQueue.Sort();
            nextQueue = new List<UnitStats>();
        }
        UnitStats contestant = turnQueue[0];
        if (contestant.gameObject.tag == "EnemyUnit") {
            waitForInput = true;
        }
        turnQueue.Remove(contestant);
        nextQueue.Add(contestant);
        print($"{contestant} up to bat.");
        if (!waitForInput) {
            NextTurn();
        }
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < turnQueue.Count; i++) {
            if (i == turnQueue.Count - 1) sb.Append($"{turnQueue[i]}"); 
            else sb.Append($"{turnQueue[i]}, ");
        }
        sb.Append("]");
        return sb.ToString();
    }

}
