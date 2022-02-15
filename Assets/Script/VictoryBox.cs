using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBox : MonoBehaviour
{
    [SerializeField] public GameObject CanvasManager;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            CanvasManager.GetComponent<ManagerCanvas>().Victory();
        }
    }
}
