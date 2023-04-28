using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDisappear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Destroy(collision.gameObject);
        }
    }
}
