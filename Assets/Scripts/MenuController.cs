using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public EnergyController energyController;
    public GameObject saveButton;
    public GameController gameController;
    public void Awake()
    {
        if (PlayerPrefs.HasKey("SaveGame"))
        {
            if(saveButton) saveButton.SetActive(true);
        }
    }

    public void StartGameButton(bool playMachine)
    {
        if (!energyController.CheckEnergyMin())
        {
            PlayerPrefs.SetInt("Machine",playMachine ? 1 : 0);
            SceneManager.LoadScene("MainGame");
        }
    }
    
    public void OptionMenuButton()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void CustomizeMenuButton()
    {
        SceneManager.LoadScene("CustomizeMenu");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SaveandExitButton()
    {
        gameController.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }


    public void ToggleGameModeChange(bool mode)
    {
        PlayerPrefs.SetInt("GameMode",mode ? 1 : 0);
        Debug.Log(mode);
    }


    public void StartSaveGameButton()
    {
        SaveGame save = JsonUtility.FromJson<SaveGame>(PlayerPrefs.GetString("SaveGame"));
        PlayerPrefs.SetInt("LoadSaveGame",1);
        PlayerPrefs.SetInt("Machine",save.gameMode);
        SceneManager.LoadScene("MainGame");
    }
}
