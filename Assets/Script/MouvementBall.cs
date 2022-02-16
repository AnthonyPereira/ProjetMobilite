using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBall : MonoBehaviour
{
    [SerializeField] public float speedBall = 10.0f;

    Rigidbody rigidbodyBall;
    float baseX;
    float baseY;
    //bool firstFrame = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyBall = GetComponent<Rigidbody>();
        baseX = Input.acceleration.x;
        baseY = Input.acceleration.y;
        Debug.Log("X: " + baseX + ", Y: " + baseY);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(firstFrame)
        {
            baseX = Input.acceleration.x;
            baseY = Input.acceleration.y;
            firstFrame = false;
            Debug.Log("X: " + baseX + ", Y: " + baseY);
        }
        */

        Vector3 dir = Vector3.zero;

        dir.x = -Input.acceleration.y - baseY;
        dir.z = Input.acceleration.x - baseX;
        dir.y = -0.5f;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        rigidbodyBall.velocity = dir * speedBall;
    }
}
