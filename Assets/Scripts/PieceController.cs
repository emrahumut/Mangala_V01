using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PieceController : MonoBehaviour
{
    public GameObject[] pits;
    public GameObject piece;
    public RuleController ruleController;
    public TurnController turnController;
    public Button UndoGameButton;
    private bool canUndo = false;
    private int[] undoPits = new int[14];

    private int gameMode;

    // Start is called before the first frame update
    void Start()
    {
        UndoGameButton.interactable = false;
        gameMode = PlayerPrefs.GetInt("GameMode");
    }

    // oyunu başlatıp 4 taş dağıtan fonksiyonumuz.
    public void GameStarter(bool saveGame = false)
    {
        UndoGameButton.interactable = false;
        if (saveGame)
        {
            SaveGame save = JsonUtility.FromJson<SaveGame>(PlayerPrefs.GetString("SaveGame"));
            for (int index = 0; index < save.pitsSave.Length; index++)
            {
                PitCleaner(index);
                for (int i = 0; i < save.pitsSave[index]; i++)
                {
                    PieceSeter(pits[index].transform);
                }
            }
        }
        else
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
    }

    // verilen taşı kuyuya yerleştirir.
    public GameObject PieceSeter(Transform pitLocation)
    {
        var newPiece = Instantiate(piece, pitLocation.Find("Pieces").position, Quaternion.identity);
        newPiece.name = "Piece";
        newPiece.transform.SetParent(pitLocation.Find("Pieces"));
        newPiece.transform.localPosition = new Vector2(UnityEngine.Random.Range(-0.045f, 0.045f),
            UnityEngine.Random.Range(-0.045f, 0.045f));
        newPiece.GetComponent<SpriteRenderer>().flipX = UnityEngine.Random.Range(0, 10) > 5;
        newPiece.GetComponent<SpriteRenderer>().flipY = UnityEngine.Random.Range(0, 10) > 5;

        return newPiece;
    }

    // Taşları kuyulara dağıtan aracı.
    public IEnumerator PieceDispenser(int startPitIndex)
    {
        turnController.turnBlocker = true;   
        if (turnController.CurrentPlayer() == Player.Player1)
        {
            for (int index = 0; index < pits.Length; index++)
            {
                undoPits[index] = pits[index].GetComponent<PitController>().PieceCount();
            }

            canUndo = true;
            UndoGameButton.interactable = enabled;
        }

        var pieceCount = PitCleaner(startPitIndex);
        var pitIndex = startPitIndex;

        if (pieceCount == 1)
        {
            pitIndex += 1;
            PieceSeter(pits[pitIndex].transform);
        }
        else
        {
            if (gameMode == 0) pitIndex++;
            GameObject[] newPieces = new GameObject[pieceCount];
            for (int i = 0; i < pieceCount; i++)
            {
                if (pitIndex > (pits.Length - 1)) pitIndex = 0;
                newPieces[i] = PieceSeter(pits[pitIndex].transform);
                newPieces[i].GetComponent<SpriteRenderer>().color = new Color(0,255,0,200); 
                pitIndex++;
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < pieceCount; i++)
            {
                if (newPieces[i])
                {
                    newPieces[i].GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
                }
            }
            pitIndex -= 1;
        }
        turnController.turnBlocker = false;
        ruleController.LastPieceEmptyPit(pitIndex, pits[pitIndex]);
        ruleController.RivalPitPieceCountEven(pitIndex, pits[pitIndex]);
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
    public IEnumerator PeicesToTreasure(int pitIndex, int treasurePitIndex)
    {
        var pieceCount = PitCleaner(pitIndex);
        GameObject[] newPieces = new GameObject[pieceCount];
        for (int i = 0; i < pieceCount; i++)
        {
            newPieces[i] = PieceSeter(pits[treasurePitIndex].transform);
            newPieces[i].GetComponent<SpriteRenderer>().color = new Color(255,255,0,200);
        }
        yield return new WaitForSeconds(3f);
        
        for (int i = 0; i < pieceCount; i++)
        {
            if (newPieces[i])
            {
                newPieces[i].GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
            }
        }
    }

    public void UndoGame()
    {
        for (int index = 0; index < undoPits.Length; index++)
        {
            PitCleaner(index);
            for (int i = 0; i < undoPits[index]; i++)
            {
                PieceSeter(pits[index].transform);
            }
        }

        undoPits = new int [14];
        canUndo = false;
        UndoGameButton.interactable = false;
    }

    // public IEnumerator PieceAnimation(GameObject newPiece, Vector3 start, Vektor3 end)
    // {
    //     while ()
    //     {
    //         
    //     }
    // }
}