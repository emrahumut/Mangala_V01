using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public Toggle gameMode;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("GameMode"))
        {
            if (PlayerPrefs.GetInt("GameMode") == 1)
            {
                gameMode.isOn = true;
            }
            else
            {
                gameMode.isOn = false;
            }
        }
    }
    
}
