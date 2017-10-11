using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables
    [Header("Bools")]
    public bool gameScene;
    public bool showOptions;
    public bool pause;
    public bool isMute;
    public bool fullScreenToggle = true;
    [Header("Keys")]
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode sprint;
    public KeyCode interact;
    //this remembers the keycode of a key 
    //we are trying to change
    public KeyCode holdingKey;
    [Header("GUI")]
    public Text forwardText;
    public Text interactText;
    public Text backwardText;
    public Text leftText;
    public Text rightText;
    public Text jumpText;
    public Text crouchText;
    public Text sprintText;
    [Header("Refrences")]
    public Slider volumeSlider, brightnessSlider;
    public AudioSource music;
    public Light dirLight;
    public GameObject menu, options;
    #endregion
    [Header("Bools")]

    [Header("Resolutions")]
    public int index;
    public int[] resX, resY;
    public Dropdown resolutionDropdown;
    //Dropdown has a value variable that you can use to reference arrays

    void Start()
    {
        #region volume

        isMute = false;

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
        #endregion
        #region SetUp Keys
        //set out keys to the preset keys we may ahve saves else set keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwards", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));

        forwardText.text = forward.ToString();
        backwardText.text = backward.ToString();
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        jumpText.text = jump.ToString();
        crouchText.text = crouch.ToString();
        sprintText.text = sprint.ToString();
        interactText.text = interact.ToString();

        #endregion
        if (gameScene)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        if (gameScene)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
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
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
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
            AudioListener.volume = 1;
            PlayerPrefs.SetFloat("Volume", music.volume);
            PlayerPrefs.SetFloat("isMute", 1);
        }
        else
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetFloat("Volume", music.volume);
            PlayerPrefs.SetFloat("isMute", 0);
        }
    }
    public void FullScreenToggle()
    {
        fullScreenToggle = !fullScreenToggle;
        Screen.fullScreen = !Screen.fullScreen;

    }
    public void ResolutionChange()
    {
        index = resolutionDropdown.value;
        Screen.SetResolution(resX[index], resY[index], fullScreenToggle);
    }
    public bool TogglePause()
    {
        if (pause)
        {
            if (!showOptions)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                menu.SetActive(false);
                pause = false;
            }
            else
            {
                showOptions = false;
                options.SetActive(false);
                menu.SetActive(true);
            }
            return false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pause = true;
            menu.SetActive(true);
            return true;
        }
    }

    #region Controls
    public void Forward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        //set our holding key to the key of this button
        {
            //set our holding key to the key of this button
            holdingKey = forward;
            //set this buttin to none allowing us to edit only this button
            forward = KeyCode.None;
            //set the GUI to blank
            forwardText.text = forward.ToString();
        }
    }
    public void Backwards()
    {
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = backward;
            //set this buttin to none allowing us to edit only this button
            backward = KeyCode.None;
            //set the GUI to blank
            backwardText.text = backward.ToString();

        }
    }
    public void Left()
    {
        if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = left;
            //set this buttin to none allowing us to edit only this button
            left = KeyCode.None;
            //set the GUI to blank
            leftText.text = left.ToString();

        }
    }
    public void Right()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = right;
            //set this buttin to none allowing us to edit only this button
            right = KeyCode.None;
            //set the GUI to blank
            rightText.text = right.ToString();

        }
    }
    public void Jump()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = jump;
            //set this buttin to none allowing us to edit only this button
            jump = KeyCode.None;
            //set the GUI to blank
            jumpText.text = jump.ToString();

        }
    }
    public void Crouch()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = crouch;
            //set this buttin to none allowing us to edit only this button
            crouch = KeyCode.None;
            //set the GUI to blank
            crouchText.text = crouch.ToString();

        }
    }
    public void Sprint()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            sprint = forward;
            //set this buttin to none allowing us to edit only this button
            sprint = KeyCode.None;
            //set the GUI to blank
            sprintText.text = sprint.ToString();

        }
    }
    public void Interact()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || forward == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = interact;
            //set this buttin to none allowing us to edit only this button
            interact = KeyCode.None;
            //set the GUI to blank
            interactText.text = interact.ToString();

        }
    }
    #endregion
    #region Key Press Event
    private void OnGUI()
    {
        Event e = Event.current;
        //if forward is set to none
        if (forward == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set Forward to new key
                    forward = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    forwardText.text = forward.ToString();
                }
            }
        if (backward == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set Forward to new key
                    backward = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    backwardText.text = backward.ToString();
                }
            }
        if (left == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set Forward to new key
                    left = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    leftText.text = left.ToString();
                }
            }
        if (right == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == forward || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set Forward to new key
                    right = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    rightText.text = right.ToString();
                }
            }
        if (crouch == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == forward || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set Forward to new key
                    crouch = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    crouchText.text = crouch.ToString();
                }
            }
        if (sprint == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == forward || e.keyCode == interact))
                {
                    //set Forward to new key
                    sprint = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    sprintText.text = sprint.ToString();
                }
            }
        if (interact == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == forward))
                {
                    //set Forward to new key
                    interact = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    interactText.text = interact.ToString();
                }
            }
        if (jump == KeyCode.None)
            if (e.isKey)
            {
                Debug.Log("Key Code: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == forward || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == forward))
                {
                    //set Forward to new key
                    jump = e.keyCode;
                    //set forward key that was pressed
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    jumpText.text = jump.ToString();
                }
            }

}
    #endregion
    /*
      RESOLUTIONS 
      3840 * 2160
      1920 * 1080

      1152 * 648
      1600 * 900
      1024 *576
   Screen.SetResolution(x,y,fullscreen(bool));
   */
}