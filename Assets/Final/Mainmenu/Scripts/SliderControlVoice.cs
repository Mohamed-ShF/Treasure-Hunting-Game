using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderControlVoice : MonoBehaviour
{
    public Slider slider;
    public AudioMixer AudioMixer;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume",0f);
    }
    public void setVolume()
    {
        float sliderValue=slider.value;
        AudioMixer.SetFloat("master",Mathf.Log10(sliderValue)*20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
   
}
