using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBall : MonoBehaviour
{
    [SerializeField] float SpeedBall = 10.0f;

    Rigidbody RigidbodyBall;

    // Start is called before the first frame update
    void Start()
    {
        RigidbodyBall = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        RigidbodyBall.velocity = dir * SpeedBall;
    }
}
