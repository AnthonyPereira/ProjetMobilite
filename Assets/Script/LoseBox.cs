using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseBox : MonoBehaviour
{
    [SerializeField] public GameObject CanvasManager;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            CanvasManager.GetComponent<ManagerCanvas>().Lose();
            //Debug.Log("Lose !!!");
        }
    }
}
