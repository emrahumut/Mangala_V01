using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class TurnController : MonoBehaviour
{
    public Enums.Player player;
    public GameObject player1;
    public GameObject player2;
    
    public void TurnStart()
    {
        if (Random.Range(0, 10) > 5)
        {
            player = Player.Player1;
            player1.SetActive(true);
        }
        else
        {
            player = Player.Player2;
            player2.SetActive(true);
            PlayMachine();
        }
    }
    // rakibe geçir
    public void TurnPass()
    {
        switch (player)
        {
            case Player.Player1 :
                player = Player.Player2;
                player1.SetActive(false);
                player2.SetActive(true);
                PlayMachine();
                break;
            case Player.Player2 :
                player = Player.Player1;
                player1.SetActive(true);
                player2.SetActive(false);
                break;
        }
    }

    // Sıra kimde
    public Player CurrentPlayer()
    {
        return player;
    }

    public void PlayMachine()
    {
        var canSelectable = new List<int>();
        for (int i = 7; i < 13; i++)
        {
            if (GameObject.Find("Pit_" + i).GetComponent<PitController>().PieceCount() != 0) canSelectable.Add(i);
        }

        if (canSelectable.Count > 0)
        {
            GameObject.Find("Pit_" + canSelectable[Random.Range(0, (canSelectable.Count))]).GetComponent<PitController>().OnMouseDown();
        }
    }
}
