using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour
{
    // Make GO persistent and load method to activate.
    // Note: if the object is not initially active, this code never runs and it is not activated in battle (facepalm)
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameObject.SetActive(false);
    }

    // callback -- avoid duplicates in the title (and duplicate callbacks?) -- otherwise activate in battle.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title") {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        } else {
            gameObject.SetActive(scene.name == "Battle");
        }
    }
}
