using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlDisappear : MonoBehaviour
{
    [SerializeField] GameObject[] pearls;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pearl"))
        {
            StartCoroutine(DetectPearls());
            Destroy(collision.gameObject);
        }
    }
    IEnumerator DetectPearls()
    {
        yield return new WaitForSeconds(1f);
        
    }
}
