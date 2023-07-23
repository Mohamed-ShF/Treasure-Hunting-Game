using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    AudioSource bgm;
    private void Awake()
    {
        bgm= GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
    public void playBGM()
    {
        if (bgm.isPlaying)
        {
            // bgm.UnPause();
            bgm.volume = 1;
        }
    }
    public void pauseBGM()
    {
        if(bgm.isPlaying)
        {
          //  bgm.Pause();
            bgm.volume= 0;
        }
    }
}
