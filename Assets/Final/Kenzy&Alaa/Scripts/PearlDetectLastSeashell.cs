using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlDetectLastSeashell : MonoBehaviour
{

    [SerializeField] Animator seaShell;
    [SerializeField] GameObject pearlPos;
    GameObject pearl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pearl = pearlPool.instance.returnPearl();
            seaShell.SetTrigger("Fire");
            pearl.transform.position = pearlPos.transform.position;
            pearl.SetActive(true);

        }
    }
}
