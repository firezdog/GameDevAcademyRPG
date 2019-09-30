using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{

    GameObject menuItem;
    public GameObject MenuItem { set => menuItem = value;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy() {
        Destroy(menuItem);
    }

}
