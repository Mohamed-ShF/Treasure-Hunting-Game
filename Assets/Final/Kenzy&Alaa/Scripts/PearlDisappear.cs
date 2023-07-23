using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlDisappear : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pearl"))
        {
            StartCoroutine(DetectPearls());
            collision.gameObject.SetActive(false);
        }
    }
    IEnumerator DetectPearls()
    {
        yield return new WaitForSeconds(1f);
        
    }
}
