using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : XMLManager
{
    public GameObject Level2;
    public GameObject Level3;
    public GameObject PlayerGameReport;

    public void ContinueAsGuest()//playerdata var passed from login scene
        //if clicked guestmod playerdata is null or empty string. if so, disable playerdata print and level choosing
    {
        if (Playerdata == null ||string.IsNullOrEmpty(Playerdata.UserName) || string.IsNullOrEmpty(Playerdata.id) )
        {
            Level2.SetActive(false);
            Level3.SetActive(false);
            PlayerGameReport.GetComponent<Text>().text = "you are in guest mod\n you dont have any data to show";

        }
    }
    public void Chooselevel()
    {
        int LevelNumber = Playerdata.MaxLevel;
        switch (LevelNumber)
        {
            case 1:
                Level2.SetActive(false);
                Level3.SetActive(false);
                break;
            case 2:
                Level2.SetActive(false);
                break;
            case 3: break;
        }
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadLevel2()
    {
        if (Level2.activeSelf == true)
            SceneManager.LoadScene(5);
    }
    public void LoadLevel3()
    {
        if(Level3.activeSelf == true)
        {
            SceneManager.LoadScene(6);
        }
    }
    public void PrintUserData()
    {
        if (Playerdata == null || Playerdata.UserName != string.Empty)
        TextToDisplay.GetComponent<Text>().text = Playerdata.ToString();
    }
    public void ExitGame()
    {
        Debug.Log("in quitting");
        Application.Quit();
    }
}
