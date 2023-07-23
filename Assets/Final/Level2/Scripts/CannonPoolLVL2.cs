using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPoolLVL2 : MonoBehaviour
{
    public static CannonPoolLVL2 instance;
    public GameObject cannonBall;
    public List<GameObject> cannonBalls;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cannonBalls= new List<GameObject>();
        GameObject temp;
        for(int i=0; i < 2; i++)
        {
            temp= Instantiate(cannonBall);
            temp.SetActive(false);
            cannonBalls.Add(temp);
        }
    }

   public GameObject returnCannonBall()
   { 
        for(int i =0; i<2; i++)
        {
            if (!cannonBalls[i].activeInHierarchy)
            {
                return cannonBalls[i];
            }
        }
        return null;
   }
}
