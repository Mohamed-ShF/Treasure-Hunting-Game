using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pearlPool : MonoBehaviour
{
    public static pearlPool instance;
    public GameObject pearl;
    public List<GameObject> pearls;

    private void Awake()
    {
        instance= this;
    }

    void Start()
    {
        pearls= new List<GameObject>();
        GameObject temp;
        for(int i = 0; i < 3; i++)
        {
            temp = Instantiate(pearl);
            temp.SetActive(false);
            pearls.Add(temp);
        }
    }
    public GameObject returnPearl()
    {
        for(int i = 0; i < 3; i++)
        {
            if (!pearls[i].activeInHierarchy)
            {
                return pearls[i];
            }
        }
        return null;

    }

}
