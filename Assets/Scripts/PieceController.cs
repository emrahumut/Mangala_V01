using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public GameObject[] pits;
    public GameObject piece;
    public RuleController ruleController;
    
    // Start is called before the first frame update
    void Start()
    {
        GameStarter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // oyunu başlatıp 4 taş dağıtan fonksiyonumuz.
    public void GameStarter()
    {
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
    }
    
    // verilen taşı kuyuya yerleştirir.
    public void PieceSeter(Transform pitLocation)
    {
        var newPiece = Instantiate(piece,pitLocation.position, Quaternion.identity);
        newPiece.transform.SetParent(pitLocation);
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
        ruleController.RivalPitPieceCount(pitIndex,pits[pitIndex]);
        ruleController.LastPieceOnTreasure(pitIndex);
    }
    
    // indisi verilen kuyudaki taşları siler.
    public int PitCleaner(int pitIndex)
    {
        var pitPieceCount = pits[pitIndex].transform.childCount;
        foreach (Transform pieces in pits[pitIndex].transform)
        {
            Destroy(pieces.gameObject);
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
