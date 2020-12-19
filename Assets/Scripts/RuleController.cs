using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    public TurnController turnControler;
    public PieceController pieceControler;

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
                if (lastPitIndex != 13)
                    turnControler.TurnPass();
                break;
        }
    }

    // Rakip kuyunun taşını çift yapma durumu.
    public void RivalPitPieceCount(int lastPitIndex, GameObject pit)
    {
        switch (turnControler.CurrentPlayer())
        {
            case Player.Player1:
                if (pit.GetComponent<PitController>().player == Player.Player2)
                {
                    if (pit.transform.childCount % 2 == 0)
                        pieceControler.PeicesToTreasure(lastPitIndex,6);
                }

                break;

            case Player.Player2:
                if (pit.GetComponent<PitController>().player == Player.Player1)
                {
                    if (pit.transform.childCount % 2 == 0)
                        pieceControler.PeicesToTreasure(lastPitIndex,13);
                }
                break;
        }
    }

    // Son taşın boş kuyuma gelmesi ve rakibin taşlarını almam
    public void LastPieceEmptyPit()
    {
    }

    // Oyun bitti mi.
    public void GameOver()
    {
    }
}