using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Niv Porat and Artiom Sheremetiev
public class scoreManager : MonoBehaviour
{
    public static scoreManager score_manager;
    public TextMeshProUGUI scoreCounter;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        if (score_manager == null)
            score_manager = this;
        scoreCounter = GetComponent<TextMeshProUGUI>();
        score = 0;
    }


    // Update is called once per frame
    public void changeScore()
    {
        score += 1;
        scoreCounter.text = "x "+ score.ToString();
    }
}
