using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] float speed;
    void Start()
    {
       rigidbody=GetComponent<Rigidbody2D>(); 
    }

    
    void Update()
    {
        rigidbody.velocity=new Vector2(-speed,0); 
    }
}
