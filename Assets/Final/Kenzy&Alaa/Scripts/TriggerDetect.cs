using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetect : MonoBehaviour
{
    [SerializeField] GameObject[] Cannonballprefab;
    [SerializeField] Transform[] ShootPoints;
    [SerializeField] AudioSource cannonAudioSource;
    [SerializeField] Animator[] CannonAnimation;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var pos0 = new Vector3(ShootPoints[0].position.x, ShootPoints[0].position.y, 0);
            var pos1 = new Vector3(ShootPoints[1].position.x, ShootPoints[1].position.y, 0);
            for(int i = 0;i < CannonAnimation.Length; i++)
            {
                CannonAnimation[i].SetTrigger("Fire");
            }
            cannonAudioSource.Play();
            Instantiate(Cannonballprefab[0],pos0,Quaternion.identity);
            Instantiate(Cannonballprefab[1], pos1, Quaternion.identity);
            
            //StartCoroutine(Destroy());



        }
    }
    IEnumerator Destroy()
    {
        
        yield return new WaitForSeconds(1.5f);
       
        for (int i=0; i<Cannonballprefab.Length; i++)
        {
            Destroy(Cannonballprefab[i]);
        }
        Debug.Log("Destroyed");

    }
}
