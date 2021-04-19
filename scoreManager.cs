using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;
    public TextMeshProUGUI scoreCounter;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        scoreCounter = GetComponent<TextMeshProUGUI>();
        score = 0;

        //scoreCounter.text = score;
    }


    // Update is called once per frame
    public void changeScore()
    {
        //score += coinValue;
        score+=1;
        scoreCounter.text = "X" + " "+ score.ToString();
    }
}
