using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
//niv porat and artiom sheremetiev
/// <summary>
/// class manages Main Menu scene, and it's functions
/// used as a GUI class, and to diffrentiate between
/// listed players and guests.
/// it inherits from XMLManager for it's use of data handling
/// which also relates to this class' purpose
/// </summary>
public class MainMenu :XMLManager
{
    //UI and game object references
    public GameObject Level2;
    public GameObject Level3;
    public GameObject PlayerGameReport;
    public GameObject HighScoreText;

    //level control variables
    private int level1BuildIndex = 4;
    private int level2BuildIndex = 5;
    private int level3BuildIndex = 6;
    private int MaxPlayersRankingDisplay = 5;

    public static bool isGuest = false;

//playerdata var passed from login scene
//if clicked guestmod playerdata is null or empty string.
//if so, disable playerdata print and level choosing
    public void ContinueAsGuest()
    {
        if (DataPlayer.Player == null || string.IsNullOrEmpty(DataPlayer.Player.UserName) || string.IsNullOrEmpty(DataPlayer.Player.id))
        {
            isGuest = true;
            DataPlayer.Player = new PlayerDataFormat();
            Level2.SetActive(false);
            Level3.SetActive(false);
            PlayerGameReport.GetComponent<Text>().text = "you are in guest mod\n you dont have any data to show";

        }
    }
    //enables level selection buttons
    //in relation to player.MaxLevel attribute
    public void Chooselevel()
    {
        int LevelNumber = 0;
        if (DataPlayer.Player == null) 
            LevelNumber = 1;
        else
            LevelNumber = DataPlayer.Player.MaxLevel;
        switch (LevelNumber)
        {
            case 1:
                Level2.SetActive(false);
                Level3.SetActive(false);
                break;
            case 2:
                Level2.SetActive(true);
                Level3.SetActive(false);
                break;
            case 3:
                Level2.SetActive(true);
                Level3.SetActive(true);
                break;
        }
    }

    //utility functions for loading different levels
    public void LoadLevel1()
    {
        SceneManager.LoadScene(level1BuildIndex);
    }
    public void LoadLevel2()
    {
        if (Level2.activeSelf == true)
            SceneManager.LoadScene(level2BuildIndex);
    }
    public void LoadLevel3()
    {
        if (Level3.activeSelf == true)
        {
            SceneManager.LoadScene(level3BuildIndex);
        }
    }

    //UI method used for showing user data and advances throughout the game
    public void PrintUserData()
    {
        if (DataPlayer.Player == null)
        {
            PlayerGameReport.GetComponent<Text>().text = "you are in guest mod\n you dont have any data to show";
            return;
        }
        TextToDisplay.GetComponent<Text>().text = DataPlayer.Player.ToString();
    }
    //closes the application
    public void ExitGame()
    {
        Debug.Log("in quitting");
        Application.Quit();
    }

    //creates another list, sorts it by using LINQ module sort
    //this sort complexity is O(N LOG N) (by C# documentation)
    //but may be invoked less than N times, this adding to
    //overall performance
    public void PrintPlayersRanking()
    {
        OpenXmlFileForReadingPlayerDB();

        TopPlayers = PlayersDB.Players.OrderByDescending(p => p.points).ToList();
        // TopPlayers= (PlayerDatabase)TopPlayers
        HighScoreText.GetComponent<Text>().text = string.Empty;
        for (int i = 0; i < MaxPlayersRankingDisplay && i < TopPlayers.Count; i++)
        {
            HighScoreText.GetComponent<Text>().text += TopPlayers[i].UserName + " " + TopPlayers[i].points + "\n\n";
        }
    }
}
