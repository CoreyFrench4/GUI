using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables
    public bool gameScene, showOptions;
    public GameObject menu, options;
    public Slider volumeSlider, brightnessSlider;
    public AudioSource music;
    public Light dirLight;
    public bool isMute;
    #endregion
    void Start()
    {

        if (PlayerPrefs.HasKey("Volume"))
        {
            Load();
        }

        if (volumeSlider != null && music != null)
        {

            volumeSlider.GetComponent<Slider>().value = music.volume;

        }

        if (brightnessSlider != null && dirLight != null)
        {
            brightnessSlider.value = dirLight.intensity;

        }

    }

    void Update()
    {
        if (volumeSlider != null && music != null)
        {
            if (music.volume != volumeSlider.value)
                music.volume = volumeSlider.value;
        }
        if (brightnessSlider != null && dirLight != null)
        {
            if (brightnessSlider.value != dirLight.intensity)
            {
                dirLight.intensity = brightnessSlider.value;
            }
        }


    }



    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }


    public void Microtransactions()
    {
        SceneManager.LoadScene(2);
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
            menu.SetActive(true);
            options.SetActive(false);
            return false;
        }
        else
        {
            showOptions = true;
            menu.SetActive(false);
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
    public void MuteToggle()
    {
        isMute = !isMute;
        if (isMute == true)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetFloat("Volume", music.volume);
            PlayerPrefs.SetFloat("isMute", 0);
        }
        else
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetFloat("Volume", music.volume);
            PlayerPrefs.SetFloat("isMute", 1);
        }
    }
}

