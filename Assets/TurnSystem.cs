using System.Collections;
using System.Collections.Generic;
using System.Text;
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
                unitStats.SetOrder(0);
                contestants.Add(unit.GetComponent<UnitStats>());
            }
        }
        contestants.Sort();
    }

    // get the next contestant to move
    void NextTurn()
    {
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (var item in contestants) sb.Append($"{item}, ");
        sb.Append("]");
        return sb.ToString();
    }

}
