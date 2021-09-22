using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// class is used for checking game requirements to load next level scene
/// using player references, time, points and lives
/// and is related to the Bus Asset - used as a end level point
/// </summary>

public class LoadNextLevel : SceneLoader //For loading next level by string or index
{
    public LivesManager LifesManager;
    public int Coins;
    public int Lives;
    private int LevelIndex;
    //private int buildIndexDifferential = 3;//build index 0 - 3 are menu and gui scenes
    private int CoinsForNextLevel = 20;
    void Start()
    {
        LevelIndex = SceneManager.GetActiveScene().buildIndex - buildIndexDifferential;// -3 cause first 3 scenes are UI scenes
    }

 
    //collision between player to bus
    //Coins and Lives get values on collision from HUD

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))//if player touched end of level collider bus
        {
            Coins = scoreManager.score_manager.score;//getting current score and lives when player collied with finish bus
            Lives = LifesManager.GetLifes();
            LevelIndex = CanPassToNextLevel();//level index gets reference to build order scene to load

            if(LevelIndex > 0)
            {
                XMLmanager.UpdatePlayerDataOnNewLevel();//Update XML when level fineshed 
                //StartCoroutine(WaitForSceneLoad());//load level delay
                SceneManager.LoadScene(LevelIndex);//load level by offset index
            }
        }
    }
    ///
    ///each level condition is 20 coins* level number
    ///1*20 = 20, 2*20 = 40, 3*20 = 60
    ///
    private int CanPassToNextLevel()
    {
        LevelIndex = SceneManager.GetActiveScene().buildIndex - buildIndexDifferential;

        if (Coins >= LevelIndex * CoinsForNextLevel && Lives > 0)//checking amout of scores and lives to get next level
            return LevelIndex + (buildIndexDifferential+1);//offset next scene index
        return 0;//not permited to load next level
    }

    //Load scene delay
    public IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(5);
    }
}
