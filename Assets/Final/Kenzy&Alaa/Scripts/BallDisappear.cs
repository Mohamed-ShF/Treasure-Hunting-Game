using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDisappear : MonoBehaviour
{
    [SerializeField] GameObject[] ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ball[0]") || collision.gameObject.CompareTag("Ball[1]"))
        {
            StartCoroutine(ballDisappear());
            Debug.Log("Destroy");
            Destroy(collision.gameObject);
        }

    }
    IEnumerator ballDisappear()
    {
        yield return new WaitForSeconds(1f);
        
    }
}
