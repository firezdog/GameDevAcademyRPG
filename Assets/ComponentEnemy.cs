using UnityEngine;

[CreateAssetMenu]
class ComponentEnemy : ScriptableObject {
	[SerializeField] GameObject enemyPrefab;
	public GameObject EnemyPrefab { get => enemyPrefab; }
	[SerializeField] private int numberToSpawn;
	public int NumberToSpawn { get => numberToSpawn; }
}