using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEncounter : MonoBehaviour
{

    [CreateAssetMenu]
    class ComponentEnemy : ScriptableObject {
        [SerializeField] GameObject enemyPrefab;
        public GameObject EnemyPrefab { get => enemyPrefab; }
        [SerializeField] private int numberToSpawn;
        public int NumberToSpawn { get => numberToSpawn; }
  }

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
                Instantiate(enemy.EnemyPrefab, gameObject.transform);
            }
        }
    }

}
