using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShipSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(muteBGM());
        }
    }
    IEnumerator muteBGM() { 
        BackgroundMusic.instance.pauseBGM();
        audioSource.Play();
        yield return new WaitForSecondsRealtime(11.6f);
        BackgroundMusic.instance.playBGM();


    }
}
