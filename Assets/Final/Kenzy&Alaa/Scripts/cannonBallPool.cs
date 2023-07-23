using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonBallPool : MonoBehaviour
{
    public static cannonBallPool instance;
    public GameObject[] cannonBall;
    public List<GameObject> cannonBalls;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cannonBalls = new List<GameObject>();
        GameObject[] temp = new GameObject[2];
        for (int i = 0; i < 2;i++)
        {
            temp[i] = Instantiate(cannonBall[i]);
            temp[i].SetActive(false);
            cannonBalls.Add(temp[i]);
        }
    }

   public GameObject returnCannonBallUp()
   {
       
         if (!cannonBalls[0].activeInHierarchy)
         {
                return cannonBalls[0];
         }
        
        return null;
   }
    public GameObject returnCannonBallDown()
    {
        if (!cannonBalls[1].activeInHierarchy)
        {
            return cannonBalls[1];
        }

        return null;
    }
}
