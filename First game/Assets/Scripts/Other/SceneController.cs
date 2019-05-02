using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject settings;
    public bool settingsMenu = false;
  
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
       

    public void ExitGame()
    {
        Application.Quit();
    }
   
}
