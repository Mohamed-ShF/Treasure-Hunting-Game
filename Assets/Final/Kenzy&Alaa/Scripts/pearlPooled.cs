using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pearlPooled : MonoBehaviour
{
    Rigidbody2D pearlRigidBody;
    public float speed;
    void Start()
    {
        pearlRigidBody= GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        pearlRigidBody.velocity = new Vector2(speed, 0);
    }
}
