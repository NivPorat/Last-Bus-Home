using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// using system time and game time
/// class controls game timer, display
/// level time and time formatting
/// </summary>
public class Timer : MonoBehaviour
{
    public static Timer instance;//instance of timer object
    public float CurrentTime;//displays current time in game
    public bool timerIsRunning = true;
    public TextMeshProUGUI TimerText;//reference for timer text display
    public int Level;//level number (1,2,3)
    private float LevelSeconds = 40f;//global var to change time for each level
    public float LevelTime;
    


    void Start()
    {
        
        Level = SceneManager.GetActiveScene().buildIndex;//level gets int from build index of level
        LevelTime = Level * LevelSeconds;
        if (instance == null)
            instance = this;

        CurrentTime = LevelTime;//current level number * defined seconds = time for each level
        TimerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
                DisplayTime(CurrentTime);
            }
            else
            {
                Debug.Log("Time has run out!");
                CurrentTime = 0;
                timerIsRunning = false;
            }
        }
    }
    //utility method to format time display
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
