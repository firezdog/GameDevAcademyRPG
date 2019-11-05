using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    UnitStats myUnit;

    public UnitStats MyUnit { get => myUnit; set => myUnit = value; }

    public void DestroyMe()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void StopAwaitingAnimation()
    {
        myUnit.StopAwaitingAnimation();
    }

    public void SetText(string text) {
        gameObject.GetComponent<Text>().text = text;
    }

}
