using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetect : MonoBehaviour
{
    [SerializeField] GameObject[] CannonballPos;
    //[SerializeField] Transform[] ShootPoints;
    [SerializeField] Animator[] CannonAnimation;

    AudioSource cannonAudioSource;
    GameObject[] Cannonballs;

    private void Start()
    {
        cannonAudioSource= GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Cannonballs = new GameObject[2];
            Cannonballs[0] = cannonBallPool.instance.returnCannonBallDown();
            Cannonballs[1] = cannonBallPool.instance.returnCannonBallUp();

            cannonAudioSource.Play();

            for (int i=0; i < 2; i++)
            {
                CannonAnimation[i].SetTrigger("Fire");
                Cannonballs[i].transform.position = CannonballPos[i].transform.position;
                Cannonballs[i].SetActive(true);
            }

            //var pos0 = new Vector3(ShootPoints[0].position.x, ShootPoints[0].position.y, 0);
            //var pos1 = new Vector3(ShootPoints[1].position.x, ShootPoints[1].position.y, 0);
            //for(int i = 0;i < CannonAnimation.Length; i++)
            //{
            //    CannonAnimation[i].SetTrigger("Fire");
            //}
            //cannonAudioSource.Play();
            //Instantiate(Cannonballprefab[0],pos0,Quaternion.identity);
            //Instantiate(Cannonballprefab[1], pos1, Quaternion.identity);
            
           


        }
    }
   
}
