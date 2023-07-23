using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDisappear : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ball[1]"))
        {
            StartCoroutine(ballDisappear());
            Debug.Log("Destroy");
            collision.gameObject.SetActive(false);

        }

    }
    IEnumerator ballDisappear()
    {
        yield return new WaitForSeconds(1f);
        
    }
}
