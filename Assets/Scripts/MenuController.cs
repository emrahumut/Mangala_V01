using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGameButton(bool playMachine)
    {
        PlayerPrefs.SetInt("Machine",playMachine ? 1 : 0);
        SceneManager.LoadScene("MainGame");
    }
    
    public void OptionMenuButton()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleGameModeChange(bool mode)
    {
        PlayerPrefs.SetInt("GameMode",mode ? 1 : 0);
        Debug.Log(mode);
    }
}
