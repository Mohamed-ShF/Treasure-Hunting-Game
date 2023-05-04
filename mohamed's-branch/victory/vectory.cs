using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class vectory : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject victoryPanel;

   public void mainmenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void creditsPanel()
    {
        victoryPanel.SetActive(false);
        creditPanel.SetActive(true);
    }
    public void backFromCredit()
    {
        creditPanel.SetActive(false);
        victoryPanel.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
