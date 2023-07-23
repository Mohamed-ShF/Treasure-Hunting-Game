using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDetectLevel2 : MonoBehaviour
{
    [SerializeField] Transform ShootPoints; 
    [SerializeField] Animator CannonAnimation;

     GameObject cannonball;
     AudioSource cannonAudioSource;

    private void Start()
    {
        cannonAudioSource= GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cannonball = CannonPoolLVL2.instance.returnCannonBall();
            cannonAudioSource.Play();
            CannonAnimation.SetTrigger("Fire");
            cannonball.transform.position = ShootPoints.position;
            cannonball.SetActive(true);

        }
    }
}
