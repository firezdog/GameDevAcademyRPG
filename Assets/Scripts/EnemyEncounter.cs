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
                newEnemy.name = $"Snake Enemy {i}";
                Vector3 newEnemyPosition = newEnemy.transform.position;
                newEnemy.transform.position = new Vector3(
                    newEnemyPosition.x,
                    newEnemyPosition.y,
                    newEnemy.transform.position.z - (i * 0.1f)
                );
            }
        }
    }

}
