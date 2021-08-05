using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    public TurnController turnControler;
    public PieceController pieceControler;
    public GameController gameController;
    
    // Son taşın hazneye gelmesi
    public void LastPieceOnTreasure(int lastPitIndex)
    {
        switch (turnControler.CurrentPlayer())
        {
            case Player.Player1:
                if (lastPitIndex != 6)
                    turnControler.TurnPass();
                break;

            case Player.Player2:
                if (lastPitIndex != 13) turnControler.TurnPass();
                else
                {
                    if (PlayerPrefs.GetInt("Machine") == 1) turnControler.PlayMachine();
                }
                break;
        }
    }

    // Rakip kuyunun taşını çift yapma durumu.
    public void RivalPitPieceCountEven(int lastPitIndex, GameObject pit)
    {
        if (pit.GetComponent<PitController>().PieceCount() % 2 == 0 && pit.GetComponent<PitController>().treasure == false)
        {
            switch (turnControler.CurrentPlayer())
            {
                case Player.Player1:
                    if (pit.GetComponent<PitController>().player == Player.Player2)
                    {
                        StartCoroutine(pieceControler.PeicesToTreasure(lastPitIndex,6));
                    }

                    break;

                case Player.Player2:
                    if (pit.GetComponent<PitController>().player == Player.Player1)
                    {
                        StartCoroutine(pieceControler.PeicesToTreasure(lastPitIndex,13));
                    }
                    break;
            }
        }
    }

    // Son taşın boş kuyuma gelmesi ve rakibin taşlarını almam
    public void LastPieceEmptyPit(int lastPitIndex, GameObject pit)
    {
        if (pit.GetComponent<PitController>().treasure == false && pit.GetComponent<PitController>().PieceCount() == 1 && pieceControler.pits[12-lastPitIndex].GetComponent<PitController>().PieceCount() != 0)
        {
            switch (turnControler.CurrentPlayer())
            {
                case Player.Player1:
                    if (pit.GetComponent<PitController>().player == Player.Player1)
                    {
                        StartCoroutine(pieceControler.PeicesToTreasure(lastPitIndex,6));
                        StartCoroutine(pieceControler.PeicesToTreasure((12 - lastPitIndex), 6));
                    }

                    break;

                case Player.Player2:
                    if (pit.GetComponent<PitController>().player == Player.Player2 && pit.GetComponent<PitController>().treasure == false)
                    {
                        StartCoroutine(pieceControler.PeicesToTreasure(lastPitIndex,13));
                        StartCoroutine(pieceControler.PeicesToTreasure((12 - lastPitIndex), 13));
                    }
                    break;
            }
        }
       
        
    }

    // Oyun bitti mi.
    public void GameOver(GameObject[] pits)
    {
        int player1Pieces = 0;
        int player2Pieces = 0;

        foreach (var pit in pits)
        {
            var pitPitController = pit.GetComponent<PitController>();

            if (pitPitController.treasure == false)
            {
                switch (pitPitController.player)
                {
                    case Player.Player1 :
                        player1Pieces += pit.GetComponent<PitController>().PieceCount();
                        break;
                    case Player.Player2 :
                        player2Pieces += pit.GetComponent<PitController>().PieceCount();
                        break;
                }
            }
        }
        
        if (player1Pieces == 0 || player2Pieces == 0)
        {
            for (int index = 0; index < pits.Length; index++)
            {
                var pitController = pits[index].GetComponent<PitController>();
                if (pitController.treasure == false)
                {
                    StartCoroutine(pieceControler.PeicesToTreasure(index,(player1Pieces == 0 ? 6 : 13)));
                }
            }

            int player1Treasure = pits[6].GetComponent<PitController>().PieceCount();
            int player2Treasure = pits[13].GetComponent<PitController>().PieceCount();
            
            if (pits[6].GetComponent<PitController>().PieceCount() > pits[13].GetComponent<PitController>().PieceCount())
            {
                Debug.Log("PLAYER - 1 WON WITH " + pits[6].GetComponent<PitController>().PieceCount());
            } 
            else if (pits[13].GetComponent<PitController>().PieceCount() > pits[6].GetComponent<PitController>().PieceCount())
            {
                Debug.Log("PLAYER - 2 WON WITH " + pits[13].GetComponent<PitController>().PieceCount());
            }
            else
            {
                Debug.Log("DRAW");
            }
            gameController.GameOver((player1Treasure >= player2Treasure  ? Player.Player1 : Player.Player2),(player1Treasure == player2Treasure));

        }
    }
}