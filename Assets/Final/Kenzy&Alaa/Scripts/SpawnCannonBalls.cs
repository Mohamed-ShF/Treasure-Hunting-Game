using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCannonBalls : MonoBehaviour
{
     Transform SpawnPoint;
    [SerializeField] GameObject cannonBall;
    [SerializeField] Animator CannonAnimator;
    void Start()
    {
        SpawnPoint= GetComponent<Transform>();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        var pos = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, 0);
        while (true)
        {
            CannonAnimator.SetTrigger("Fire");
           
            Instantiate(cannonBall, pos, Quaternion.identity);
            
            yield return new WaitForSeconds(2f);

        }
        
    }
}
