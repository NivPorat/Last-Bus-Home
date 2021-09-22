using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// class related to UI pause button
/// detects key press and handles game timescale
/// </summary>
public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;//pause pushed flag
    public GameObject pauseMenuUI;//reference to pause UI


    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//call pause by ESC button
        {
            
            if (GameIsPaused)//if pushed once more ESC return to game
            {
                Resume();
            }
            else//Pause
            {
                Pause();
            }
       }
    }
    //resumes the game, setting timescale to 1
    public void Resume()
    {
        pauseMenuUI.SetActive(false);//remove UI of pause
        Time.timeScale = 1f;//set timer time to normal time
        GameIsPaused = false;
    }
    //stops game, system timescale = 0
    public void Pause()
    {
        pauseMenuUI.SetActive(true);//show UI of pause
        Time.timeScale = 0f;//stop system time
        GameIsPaused = true;
    }
    //quits current game, load main menu
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");//load main menu without saving
    }
}
