using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitController : MonoBehaviour
{
    public PieceController pieceController;
    public TurnController turnController;
    public Enums.Player player;
    public bool treasure;
    private void OnMouseDown()
    {
        if (turnController.CurrentPlayer() == player)
        {
            if(treasure == false) pieceController.PieceDispenser(int.Parse(gameObject.name.Split('_')[1]));
        }
    }
}
