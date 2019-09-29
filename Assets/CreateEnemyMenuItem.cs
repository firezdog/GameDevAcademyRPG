﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemyMenuItem : MonoBehaviour
{
    [SerializeField] GameObject targetEnemyButtonPrefab;
    GameObject enemyUnitsMenu;

    void Start()
    {
        enemyUnitsMenu = GameObject.Find("EnemyUnitsMenu");
        GameObject myEnemyButton = Instantiate(targetEnemyButtonPrefab, enemyUnitsMenu.transform);
        myEnemyButton.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

}