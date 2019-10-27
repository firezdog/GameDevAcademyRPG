using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUnit : MonoBehaviour
{
    [SerializeField] Image unitPicture;
    internal void Activate(UnitStats activePlayer)
    {
        gameObject.SetActive(true);
        unitPicture.sprite = activePlayer.Portrait;
    }

    internal void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
