using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseMenu : MonoBehaviour
{
    public Slider volumeSlider, brightnessSlider;
    public AudioSource music;
    public Light dirLight;
    public bool gameScene, showOptions;
    public GameObject pause, options;
   
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }
    public void ShowOptions()
    {
        ToggleOptions();
    }

    public bool ToggleOptions()
    {
        if (showOptions)
        {
            showOptions = false;
            pause.SetActive(true);
            options.SetActive(false);
            return false;
        }
        else
        {
            showOptions = true;
           pause.SetActive(false);
            options.SetActive(true);
            return true;
        }
    
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", music.volume);
        PlayerPrefs.SetFloat("Brightness", dirLight.intensity);
    }
    public void Load()
    {
        music.volume = PlayerPrefs.GetFloat("Volume");
        dirLight.intensity = PlayerPrefs.GetFloat("Brightness");
    }
    public void Default()
    {
        volumeSlider.value = 1;
        brightnessSlider.value = 1;
        PlayerPrefs.SetFloat("Volume", 1);
        PlayerPrefs.SetFloat("Brightness", 1);
    }

}

    
    
    


