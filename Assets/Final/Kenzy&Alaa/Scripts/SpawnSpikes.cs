using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
     GameObject spike;
     Transform spawnPoint;
     Vector3 pos;
    void Start()
    {
        spawnPoint = transform;
        pos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0);
        StartCoroutine(spawn());
    }

   
    IEnumerator spawn()
    {
        
        while (true)
        {
            spike = spikePool.instance.returnSpiks();

            if (gameObject.CompareTag("left"))
            {
                spike.GetComponent<Spike>().speed = 3f;
            }
            else if (gameObject.CompareTag("right"))
            {
                spike.GetComponent<Spike>().speed = -3f;

            }
            spike.transform.position = pos;
            spike.SetActive(true);
            yield return new WaitForSecondsRealtime(1.8f);
        }
        
  
    }
}
