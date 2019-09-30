using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] GameObject enemyEncounter;
    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        if (triggered) Instantiate(enemyEncounter);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            triggered = true;
            DontDestroyOnLoad(gameObject);  
            SceneManager.LoadScene("Battle");
        } 
    }

}
