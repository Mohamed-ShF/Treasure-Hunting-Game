using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ControlHealthBar : MonoBehaviour
{
    [SerializeField] Player2Movement player;
    [SerializeField] Image healthBarImage;


    private void Start()
    {
        healthBarImage.type = Image.Type.Filled;
        healthBarImage.fillMethod = Image.FillMethod.Horizontal;
        healthBarImage.fillOrigin = (int)Image.OriginHorizontal.Left;
    }
    public void UpdateHealthBar()
    {
       
        float duration =0.75f * (player.currentHealth / player.maxHealth);
         healthBarImage.fillAmount = Mathf.Clamp(player.currentHealth / player.maxHealth, 0f, 1f);
        //healthBarImage.DOFillAmount(player.currentHealth / player.maxHealth, duration);
        Debug.Log(player.currentHealth);
        Color newColor = Color.green;
        if (player.currentHealth < player.maxHealth * 0.25f)
        {
            newColor = Color.red;
        }
        else if (player.currentHealth < player.maxHealth * 0.6f)
        {
            newColor = Color.yellow;
        }
       // healthBarImage.DOColor(newColor, duration);

        // healthBarImage.value = amount;
    }
}
