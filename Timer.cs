using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float CurrentTime;
    public bool timerIsRunning = true;
    public TextMeshProUGUI TimerText;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        CurrentTime = 10.0f;
        TimerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
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

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
