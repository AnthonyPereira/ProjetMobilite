using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private DataLevels Data;

    void Start() {
        Data = GameObject.Find("GenerateLevels").GetComponent<DataLevels>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(!Data) return;

        if(col.tag == "Player")
        {
            Data.AddCoins();
            Destroy(gameObject);
        }
    }
}
