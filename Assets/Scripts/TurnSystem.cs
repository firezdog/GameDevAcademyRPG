using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TurnSystem : MonoBehaviour
{

    [SerializeField] GameObject battleHUD, actionsMenu, enemyUnitsMenuPrefab, targetEnemyButton, loseMenu;
    [SerializeField] DisplayUnit characterDisplay;

    GameObject enemyUnitsMenu = null;

    List<UnitStats> turnQueue;
    List<UnitStats> nextQueue;
    private UnitStats contestant;

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
        Destroy(enemyUnitsMenu);
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
        contestant = turnQueue[0];
        if (contestant.tag == "EnemyUnit") TurnOffMenus();
        else if (contestant.tag == "PlayerUnit") StartPlayerTurn(contestant);
        turnQueue.Remove(contestant);
        nextQueue.Add(contestant);
        contestant.Act(this);
    }

    // create the enemy selection menu and associate each entry with the proper target in such a way that, when clicked on,
    // the correct enemy is then attacked.
    public void SelectAttack()
    {
        Destroy(enemyUnitsMenu);
        enemyUnitsMenu = Instantiate(enemyUnitsMenuPrefab, gameObject.transform.parent.transform, false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        Attack contestantAttack = contestant.gameObject.GetComponent<Attack>();
        foreach (GameObject enemy in enemies) 
        {
            GameObject newEnemyButton = Instantiate(targetEnemyButton, enemyUnitsMenu.transform, false);
            newEnemyButton.GetComponent<Image>().sprite = enemy.GetComponent<UnitStats>().Portrait;
            newEnemyButton.GetComponent<Button>().onClick.AddListener(
                delegate { StartCoroutine(contestantAttack.AttackTarget(this, enemy)); }
            );
        }
    }

    public override string ToString() 
    {
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
