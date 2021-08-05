using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public Toggle gameMode;
    public GameObject bgImageButton;
    public Sprite[] bgImages;
    
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
        BgColor();
    }

    public void BgColor()
    {
        GameObject bgImageContent = GameObject.Find("BgImagesContent");
        foreach (Sprite image in bgImages)
        {
            GameObject button = Instantiate(bgImageButton, bgImageContent.transform);
            button.GetComponent<Image>().sprite = image;
            button.GetComponent<Button>().onClick.AddListener(() => { BgSetter(image);});
        }
    }

    public void BgSetter(Sprite image)
    {
        PlayerPrefs.SetString("BgImage",image.name);
    }
    
}
