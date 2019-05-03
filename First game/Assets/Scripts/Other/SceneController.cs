using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject settings;
    public GameObject pauseMenu;
    public bool settingsMenu = false;
    public bool pause= false;


  
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void SettingsMenu()
    {
        if (!settingsMenu)
        {
            settings.SetActive(!settingsMenu);
            settingsMenu = true;
        }
        else
        {
            settings.SetActive(!settingsMenu);
            settingsMenu = false;
        }
    }

    public void Music()
    {
    
    }

    public void SFX()
    {

    }
       
    public void Pause()
    {
        pause = !pause;
        if (pause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(settingsMenu=false);
            settings.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
   
}
