using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorController : MonoBehaviour
{
    public GameObject bgImageButton;
    public Sprite[] bgImages;

    public GameObject boardImageButton;
    public Sprite[] boardImages;

    public GameObject pieceImageButton;
    public Sprite[] pieceImages;
    public void Start()
    {
        BgImages();
        BoardImages();
        PieceImages();
    }

    public void BgImages()
    {
        GameObject bgImageContent = GameObject.Find("BgImagesContent");
        foreach (Sprite image in bgImages)
        {
            GameObject button = Instantiate(bgImageButton, bgImageContent.transform);
            button.GetComponent<Image>().sprite = image;
            button.GetComponent<Button>().onClick.AddListener(() => { BgSetter(image);});
            // button.GetComponent<Button>().interactable =                 
            //     PlayerPrefs.HasKey("BGImage")                      
            //         ? PlayerPrefs.GetString("BGImage") == image.name ? false : true                      
            //         : true;
        }
    }

    public void BgSetter(Sprite image)
    {
        PlayerPrefs.SetString("BgImage",image.name);
    }
    
    public void BoardImages()
    {
        GameObject content = GameObject.Find("BoardImagesContent");
        foreach (Sprite image in boardImages)
        {
            GameObject button = Instantiate(boardImageButton, content.transform);
            button.GetComponent<Image>().sprite = image;
            button.GetComponent<Button>().onClick.AddListener(() => { BoardImageSetter(image);});
        }
    }
    
    public void BoardImageSetter(Sprite image)
    {
        PlayerPrefs.SetString("BoardImage",image.name);
    }
    
    public void PieceImages()
    {
        GameObject content = GameObject.Find("PieceImagesContent");
        foreach (Sprite image in pieceImages)
        {
            GameObject button = Instantiate(pieceImageButton, content.transform);
            button.GetComponent<Image>().sprite = image;
            button.GetComponent<Button>().onClick.AddListener(() => { PieceImageSetter(image);});
        }
    }

    public void PieceImageSetter(Sprite image)
    {
        PlayerPrefs.SetString("PieceImage",image.name);
    }
}
