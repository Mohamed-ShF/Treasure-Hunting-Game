using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeSpawnWithPooling : MonoBehaviour
{
    GameObject spike;
    Transform spawnPoint;
    void Start()
    {
        spawnPoint = transform;
        StartCoroutine(spawn());
    }

    
    IEnumerator spawn()
    {

        var pos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0);
        while (true)
        {
            spike = spikesPooling.instance.returnSpikes();
            if (gameObject.CompareTag("left"))
            {
                spike.GetComponent<Spike>().speed = 3f;
            }
            else if(gameObject.CompareTag("right"))
            {
                spike.GetComponent<Spike>().speed = -3f;

            }
            spike.transform.position = pos;
            spike.SetActive(true);
            yield return new WaitForSecondsRealtime(1.8f);
        }


    }
}
