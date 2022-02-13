using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider col){
        if(col.tag == "Player"){
            Debug.Log("Victory");
            Application.Quit();
        }
    }
}
