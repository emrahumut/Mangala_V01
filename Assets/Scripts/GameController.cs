using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SaveGame
{
    public int[] pitsSave;
    public float player1ScoreSave;
    public float player2ScoreSave;
    public int playedSetSave;
    public int gameMode;
    public Player currentPlayer;
}

public class GameController : MonoBehaviour
{
    public PieceController pieceController;
    public EnergyController energyController;
    public TurnController turnController;
    public int set;
    public float winnerScore;
    public float loserScore;
    public float drawScore;

    private int playedSet = 0;
    private float player1Score = 0;
    private float player2Score = 0;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text setText;
    
    public GameObject background;
    public GameObject board;
    public GameObject piece;
    public void Awake()
    {
        energyController.Awake();

        if (PlayerPrefs.HasKey("LoadSaveGame"))
        {
            SaveGame save = JsonUtility.FromJson<SaveGame>(PlayerPrefs.GetString("SaveGame"));
            player1Score = save.player1ScoreSave;
            player2Score = save.player2ScoreSave;
            playedSet = save.playedSetSave - 1;
        }
        else
        {
            energyController.UseEnergy(1);
        }

        if (PlayerPrefs.HasKey("BgImage"))
        {
            Debug.Log(PlayerPrefs.GetString("BgImage"));
            background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Bg/" + PlayerPrefs.GetString("BgImage"));
            var scaleRef = 16f / 9f;
            var bgScale = background.transform.localScale.x;
            var scale = (Screen.width * 1f) / (Screen.height * 1f);
            background.transform.localScale =
                new Vector2(((scale * bgScale) / scaleRef), ((scale * bgScale) / scaleRef));
        }

        if (PlayerPrefs.HasKey("BoardImage"))
        {
            board.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Boards/" + PlayerPrefs.GetString("BoardImage"));
        }

        if (PlayerPrefs.HasKey("PieceImage"))
        {
            piece.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Piece/" + PlayerPrefs.GetString("PieceImage"));
        }
        else
        {
            background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Bg/bg.png");
        }
        
    }

    public void Start()
    {
        playedSet++;
        setText.text = "SET : " + playedSet + " / " + set;
        player1ScoreText.text = "SKOR : " + player1Score;
        player2ScoreText.text = "SKOR : " + player2Score;
        if (PlayerPrefs.HasKey("LoadSaveGame"))
        {
            pieceController.GameStarter(true);
            PlayerPrefs.DeleteKey("LoadSaveGame");
        }
        else
        {
            pieceController.GameStarter();
        }
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

    public void SaveGame()
    {
        SaveGame save = new SaveGame();

        save.pitsSave = new int[14];

        for (int index = 0; index < save.pitsSave.Length; index++)
        {
            save.pitsSave[index] = pieceController.pits[index].GetComponent<PitController>().PieceCount();
        }

        save.player1ScoreSave = player1Score;
        save.player2ScoreSave = player2Score;
        save.playedSetSave = playedSet;
        save.currentPlayer = turnController.CurrentPlayer();
        save.gameMode = PlayerPrefs.GetInt("Machine");
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(save));
    }
    public void OnApplicationQuit()
    {
        SaveGame();
    }
}