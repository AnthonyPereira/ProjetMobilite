using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBall : MonoBehaviour
{
    [SerializeField] public float speedBall = 10.0f;

    Rigidbody rigidbodyBall;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyBall = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        dir.y = -0.2f;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        rigidbodyBall.velocity = dir * speedBall;
    }
}
