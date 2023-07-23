using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikePool : MonoBehaviour
{
    public static spikePool instance;
    public GameObject spike;
    public List<GameObject> spikes;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        spikes= new List<GameObject>();
        GameObject temp;
        for(int i = 0 ; i < 12 ; i++)
        {
            temp = Instantiate(spike);
            temp.SetActive(false);
            spikes.Add(temp);

        }
        
    }

    public GameObject returnSpiks()
    {
        for(int i = 0; i < 12; i++)
        {
            if (!spikes[(int)i].activeInHierarchy)
            {
                return spikes[(int)i];
            }
        }
        return null;
    }
    
}
