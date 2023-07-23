using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeDisappearWithPooling : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpikePool"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
