using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ControlHealthBar1 : MonoBehaviour
{
    [SerializeField] Player2Movement player;
    [SerializeField] Slider healthBarImage;
    [SerializeField] Image fill;
    private void Start()
    {
        healthBarImage.value= 1;
    }


    public void UpdateHealthBar()
    {
       
        float amount =  (player.currentHealth / player.maxHealth);
        if (amount < 0.7f && amount > 0.2f)
        {
            fill.color = Color.yellow;
        }
        else if (amount <= 0.2f)
        {
            fill.color = Color.red;
        }
        else if(amount >= 0.7f)
        {
            fill.color = Color.green;
        }
       

         healthBarImage.value = amount;
    }
}
