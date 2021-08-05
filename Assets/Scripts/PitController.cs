using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class PitController : MonoBehaviour
{
    public PieceController pieceController;
    public TurnController turnController;
    public Enums.Player player;
    public GameObject piecesObject;
    public Text pieceCountText;
    public bool treasure;

    public void Update()
    {
        pieceCountText.text = piecesObject.transform.childCount.ToString();
    }

    public void OnMouseDown()
    {
        if (turnController.turnBlocker == false)
        {
            PieceCount();
            if (turnController.CurrentPlayer() == player && PieceCount() != 0)
            {
                if(treasure == false) StartCoroutine(pieceController.PieceDispenser(int.Parse(gameObject.name.Split('_')[1])));
            }
        }
    }

    public int PieceCount()
    {
        return piecesObject.transform.childCount;
    }

    
}
