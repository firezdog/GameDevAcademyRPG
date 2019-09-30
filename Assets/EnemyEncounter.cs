using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEncounter : MonoBehaviour
{

    [SerializeField] ComponentEnemy[] spawnList;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        foreach(ComponentEnemy enemy in spawnList)
        {
            for (int i = 0; i < enemy.NumberToSpawn; i++)
            {
                GameObject newEnemy = Instantiate(enemy.EnemyPrefab, gameObject.transform);
                
                newEnemy.transform.position = new Vector3(
                    newEnemy.transform.position.x,
                    newEnemy.transform.position.y,
                    newEnemy.transform.position.z - i
                );
            }
        }
    }

}
