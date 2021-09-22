using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//niv porat and artiom sheremetiev
/// <summary>
/// class related to Game Over UI Screen
/// handles GameTime and game timescale
/// </summary>
public class GameOverMenu : MonoBehaviour
    
{
    public static bool GameIsPaused = false;//pause pushed flag
    public GameObject GameOverMenuUI;//reference to pause UI
    public LivesManager LivesManager;
    public Timer timer;
    int level;


    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;  
    }

    // Update is called once per frame
    void Update()
    {
        if (LivesManager.Life == 0 || timer.CurrentTime == 0f)//call pause by ESC button
        {
                Pause();
        }
    }

   

    public void Pause()
    {
        GameOverMenuUI.SetActive(true);//show UI of pause
        Time.timeScale = 0f;//stop system time
        GameIsPaused = true;
    }
    public void ReloadCurrentLevel()
    {
        //DontDestroyOnLoad(DataPlayer.Player);
        
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
       SceneManager.LoadScene("MainMenu");//load main menu without saving
    }
}
