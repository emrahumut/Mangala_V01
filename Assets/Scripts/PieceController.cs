using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceController : MonoBehaviour
{
    public GameObject[] pits;
    public GameObject piece;
    public RuleController ruleController;
    public TurnController turnController;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     GameStarter();
    // }
    
    // oyunu başlatıp 4 taş dağıtan fonksiyonumuz.
    public void GameStarter()
    {
        for (int index = 0; index < pits.Length; index++)
        {
            PitCleaner(index);
        }
      
        // Hazine dışındaki kuyularda 4 tane taş oluşturucu.
        for (int index = 0; index < pits.Length; index++)
        {
            // Hazinelere taş koymaz.
            if (index == 13 || index == 6) continue;
            
            for (int j = 1; j <= 4; j++)
            {
                PieceSeter(pits[index].transform);
            }
        }

        turnController.TurnStart();
    }
    
    // verilen taşı kuyuya yerleştirir.
    public void PieceSeter(Transform pitLocation)
    {
        var newPiece = Instantiate(piece,pitLocation.Find("Pieces").position, Quaternion.identity);
        newPiece.name = "Piece";
        newPiece.transform.SetParent(pitLocation.Find("Pieces"));
        newPiece.transform.localPosition = new Vector2(UnityEngine.Random.Range(-0.05f,0.05f),UnityEngine.Random.Range(-0.05f,0.05f));
        
    }
    
    // Taşları kuyulara dağıtan aracı.
    public void PieceDispenser(int startPitIndex)
    {
        var pieceCount = PitCleaner(startPitIndex);
        var pitIndex = startPitIndex;
        if (pieceCount == 1)
        {
            pitIndex += 1;
            PieceSeter(pits[pitIndex].transform);
        }
        else
        {
            for (int i = 0; i <  pieceCount; i++)
            {
                if (pitIndex > (pits.Length-1)) pitIndex = 0;
                PieceSeter(pits[pitIndex].transform);
                pitIndex++;
            }

            pitIndex -= 1;
        }
        ruleController.LastPieceEmptyPit(pitIndex, pits[pitIndex]);
        ruleController.RivalPitPieceCountEven(pitIndex,pits[pitIndex]);
        ruleController.LastPieceOnTreasure(pitIndex);
        ruleController.GameOver(pits);
    }
    
    // indisi verilen kuyudaki taşları siler.
    public int PitCleaner(int pitIndex)
    {
        var pitPieceCount = pits[pitIndex].GetComponent<PitController>().PieceCount();
        for (int i = pitPieceCount - 1; i >= 0; i--)
        {
            DestroyImmediate(pits[pitIndex].transform.Find("Pieces").GetChild(i).gameObject);
        }
        
        return pitPieceCount;
    }
    
    // Belirtildiği kadar taşları hazineye taşır.
    public void PeicesToTreasure(int pitIndex,int treasurePitIndex)
    {
        var pieceCount = PitCleaner(pitIndex);
        for (int i = 0; i < pieceCount; i++)
        {
            PieceSeter(pits[treasurePitIndex].transform);
        }
    }
    
}
