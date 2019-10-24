using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUnit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Activate(UnitStats activePlayer)
    {
        gameObject.SetActive(true);
        print($"{activePlayer.name}");
    }

    internal void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
