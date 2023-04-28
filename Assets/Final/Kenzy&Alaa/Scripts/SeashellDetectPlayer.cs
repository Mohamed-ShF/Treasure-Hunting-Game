using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeashellDetectPlayer : MonoBehaviour
{
    [SerializeField] Animator[] seaShell;
    [SerializeField] Transform[] pearlPoints;
    [SerializeField] GameObject[] pearl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var pos0 = new Vector3(pearlPoints[0].position.x, pearlPoints[0].position.y, 0);
            var pos1 = new Vector3(pearlPoints[1].position.x, pearlPoints[1].position.y, 0);
           // var pos2 = new Vector3(pearlPoints[2].position.x, pearlPoints[2].position.y, 0);

            for (int i = 0;i < pearl.Length; i++)
            {
                seaShell[i].SetTrigger("Fire");
            }

            Instantiate(pearl[0], pos0, Quaternion.identity);
            Instantiate(pearl[1], pos1, Quaternion.identity);
           // Instantiate(pearl[2], pos2, Quaternion.identity);


        }
    }
}
