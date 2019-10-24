using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnSystem : MonoBehaviour
{

    [SerializeField] GameObject actionsMenu, enemyUnitsMenu, loseMenu;
    [SerializeField] DisplayUnit characterDisplay;

    List<UnitStats> turnQueue;
    List<UnitStats> nextQueue;

    // Start is called before the first frame update
    void Start()
    {
        GetAllContestants();
        NextTurn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TurnOffMenus() {
        actionsMenu.SetActive(false);
        enemyUnitsMenu.SetActive(false);
        characterDisplay.Deactivate();
    }

    void StartPlayerTurn(UnitStats activePlayer) {
        characterDisplay.Activate(activePlayer);
        actionsMenu.SetActive(true);
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

    public void RemoveUnitFromBattle(UnitStats unit) 
    {
        turnQueue.Remove(unit);
        nextQueue.Remove(unit);
    }

    // get the next contestant to move
    public void NextTurn()
    {
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
        if (contestant.tag == "EnemyUnit") TurnOffMenus();
        else if (contestant.tag == "PlayerUnit") StartPlayerTurn(contestant);
        turnQueue.Remove(contestant);
        nextQueue.Add(contestant);
        contestant.Act(this);
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

    public void Lose() 
    {
        foreach(Animator a in FindObjectsOfType<Animator>())
        {
            a.enabled = false;
        }
        loseMenu.SetActive(true);
    }

    public void Flee()
    {
        SceneManager.LoadScene("Town");
    }

}
