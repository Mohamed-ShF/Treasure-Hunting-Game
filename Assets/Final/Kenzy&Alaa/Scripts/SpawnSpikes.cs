using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
    [SerializeField] GameObject spike;
    Transform spawnPoint;
    void Start()
    {
        spawnPoint = transform;
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawn()
    {
        
        var pos = new Vector3(spawnPoint.position.x, spawnPoint.position.y,0);
        while (true)
        {
            Instantiate(spike, pos, Quaternion.identity);
            yield return new WaitForSecondsRealtime(1.8f);
        }
        
  
    }
}
