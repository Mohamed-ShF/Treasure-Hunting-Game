using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SeashellDetectPlayer : MonoBehaviour
{
    [SerializeField] Animator[] seaShell;
    [SerializeField] GameObject[] pearlPos;
     GameObject[] pearls;
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
           pearls= new GameObject[2];

            for (int i = 0; i < 2; i++)
            {
                seaShell[i].SetTrigger("Fire");
                pearls[i] = pearlPool.instance.returnPearl();
                if (pearlPos[i].CompareTag("left"))
                {
                    pearls[i].GetComponent<pearlPooled>().speed = -36;
                }
                else if (pearlPos[i].CompareTag("right"))
                {
                    pearls[i].GetComponent<pearlPooled>().speed = 36;
                }
                pearls[i].transform.position = pearlPos[i].transform.position;
                pearls[i].SetActive(true);
            }
           

          

            
        }
    }
}
