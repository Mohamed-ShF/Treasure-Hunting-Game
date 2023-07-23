using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPearls : MonoBehaviour
{
    GameObject pearl;
    Transform spawnPoint;
    Vector3 pos;
    void Start()
    {
        spawnPoint = transform;
        pos= transform.position;

    }
    public void spawn()
    {
        pearl = pearlPool.instance.returnPearl();

        if (gameObject.CompareTag("left"))
        {
            pearl.GetComponent<pearlPooled>().speed = -36;
        }
        else if (gameObject.CompareTag("right"))
        {
            pearl.GetComponent<pearlPooled>().speed = 36;
        }

        pearl.transform.position = pos;
        pearl.SetActive(true);
    }
  
         
        
}
