using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TurnController : MonoBehaviour
{
    public Enums.Player player;
    public GameObject player1;
    public GameObject player2;
    public bool turnBlocker;
    private bool played = true;
    public void TurnStart()
    {
        if (Random.Range(0, 10) > 5)
        {
            player = Player.Player1;
            player1.GetComponent<Image>().color = new Color(0,255,0,150);
            
        }
        else
        {
            player = Player.Player2;
            player2.GetComponent<Image>().color = new Color(0,255,0,150);
            if (PlayerPrefs.GetInt("Machine") == 1) PlayMachine();
        }
    }

    // rakibe geçir
    public void TurnPass()
    {
        switch (player)
        {
            case Player.Player1:
                player = Player.Player2;
                player1.GetComponent<Image>().color = new Color(255,255,255,255);

                player2.GetComponent<Image>().color = new Color(0,255,0,150);
                if (PlayerPrefs.GetInt("Machine") == 1) PlayMachine();
                break;
            case Player.Player2:
                player = Player.Player1;
                player1.GetComponent<Image>().color = new Color(0,255,0,150);
                player2.GetComponent<Image>().color = new Color(255,255,255,255);

                break;
        }
    }

    // Sıra kimde
    public Player CurrentPlayer()
    {
        return player;
    }
    public void SetCurrentPlayer(Player currentPlayer)
    {
        player = currentPlayer;
        switch (player)
        {
            case Player.Player1:
        
                player1.GetComponent<Image>().color = new Color(0,255,0,150);
                break;
            case Player.Player2:
       
                player2.GetComponent<Image>().color = new Color(0,255,0,150);
                break;
        }
    }

    public void PlayMachine()
    {
        var canSelectable = new List<int>();
        played = true;
        for (int i = 7; i < 13; i++)
        {
            if (GetPieceCount(i) != 0) canSelectable.Add(i);
        }

        if (canSelectable.Count > 0)
        {
            for (int yapayZekaninIndisi = 7; yapayZekaninIndisi < 13; yapayZekaninIndisi++)
            {
                int wPlayingPit = (yapayZekaninIndisi + GetPieceCount(yapayZekaninIndisi) - 1);
                int oynancakKuyununIndisi = (wPlayingPit - 14);

                // son taş kuyuya denk geliyor mu? 
           
                
                // rakibin kuyusunu çift yapma
                
                if (wPlayingPit > 13)
                {
                    // int oynancakKuyudakiTasSayisi = GetPieceCount(oynancakKuyununIndisi);
                    if ((GetPieceCount(oynancakKuyununIndisi) + 1) % 2 == 0)
                    {
                        Move(yapayZekaninIndisi);
                        break;
                    }
                }
          
                else if (wPlayingPit == 13)
                {
                    Move(yapayZekaninIndisi);
                    break;
                }
                
                // Son taş boşluğa gelip rakibin kuyusndaki taşları alabiliyor muyum
                else if (wPlayingPit < 13)
                {
                    if (GetPieceCount(wPlayingPit) == 0 && GetPieceCount(13 - wPlayingPit) != 0)
                    {
                        Move(yapayZekaninIndisi);
                        break;
                    }
                }
            }

            if (played)
            {
                GameObject.Find("Pit_" + canSelectable[Random.Range(0, (canSelectable.Count))])
                    .GetComponent<PitController>().OnMouseDown();
            }
           
        }
    }
    public void Move(int pitIndis)
    {
        GameObject.Find("Pit_" + pitIndis)
            .GetComponent<PitController>().OnMouseDown();
        played = false;
    }
    public int GetPieceCount(int pitIndex)
    {
        return GameObject.Find("Pit_" + pitIndex).GetComponent<PitController>().PieceCount();
    }
}