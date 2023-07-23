using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesPooling : MonoBehaviour
{
    public static spikesPooling instance;
    public GameObject spike;
    public List<GameObject> spikes;
   
    public void Awake()
    {
        instance= this;
    }
    void Start()
    {
        spikes = new List<GameObject>();
        GameObject temp;
        for(int i=0; i<5; i++)
        {
            temp= Instantiate(spike);
            temp.SetActive(false);
            spikes.Add(temp);
         
        }
        
    }
    public GameObject returnSpikes()
    {
        for(int i=0;i<5;i++) {
            if (!spikes[i].activeInHierarchy)
            {
                Debug.Log("notActive");
                return spikes[(int)i];
            }
           
        }
        return null;
    }
    
}
