using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
   public PieceController pieceController;
   public int set;
   public float winnerScore;
   public float loserScore;
   public float drawScore;

   private int playedSet = 0;
   private float player1Score;
   private float player2Score;
   public Text player1ScoreText;
   public Text player2ScoreText;
   public Text setText;
   
   public void Start()
   {
      playedSet++;
      setText.text = "Set: " + playedSet + "/" + set;
      player1ScoreText.text = "Player 1 Score: " + player1Score;
      player2ScoreText.text = "Player 2 Score: " + player2Score;
      
      pieceController.GameStarter();
   }

   public void GameOver(Player player, bool draw)
   {
      if (draw)
      {
         player1Score += drawScore;
         player2Score += drawScore;
      }
      else
      {
         switch (player)
         {
            case Player.Player1:
               player1Score += winnerScore;
               player2Score -= loserScore;
               break;
            case Player.Player2:
               player2Score += winnerScore;
               player1Score -= loserScore;
               break;
         }
      }
      
      if (set > playedSet)
      {
         Start();
      }
      else
      {
         setText.text = "Set: " + playedSet + "/" + set;
         player1ScoreText.text = "Player 1 Score: " + player1Score;
         player2ScoreText.text = "Player 2 Score: " + player2Score;
         Debug.Log((player1Score > player2Score ? "player 1 kazandı" : "player 2 kazandı"));
      }
   }
}
