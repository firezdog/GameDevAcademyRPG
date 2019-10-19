using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{

    public void DestroyMe()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void SetText(string text) {
        gameObject.GetComponent<Text>().text = text;
    }

}
