using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    public GameObject bullet;
    public List<GameObject> bullets;

    private void Awake()
    {
        instance= this;
    }
    void Start()
    {
        bullets= new List<GameObject>();
        GameObject temp;
        for(int i = 0; i < 3; i++)
        {
            temp = Instantiate(bullet);
            temp.SetActive(false);
            bullets.Add(temp);
        }
        
    }

    public GameObject returnBullet()
    {
        for(int i = 0; i < 3; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }
        return null;
    }
}
