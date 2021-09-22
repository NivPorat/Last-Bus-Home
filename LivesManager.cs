using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// Object in unity can't be created by code,so we need static class for UI 
/// class manages player lives, it's instances in code and game
/// and manages life amount, counter and objects related to it
/// </summary>
public class LivesManager : MonoBehaviour
{
    public static LivesManager livesManager;//Singleton
    public TextMeshProUGUI lives_counter;//score text
    public int Life;//Amount of lives
    private int StartingLife = 3;
   
    void Start()
    {
        Life = StartingLife;//seting amount of lives
        lives_counter = GameObject.Find("lives_counter").GetComponent<TextMeshProUGUI>();//reference of text where is printing lives amount
        lives_counter.text = "x " + Life;// print lives to UI
    }
    public int GetLifes()
    {
        return Life;//get amount of lives
    }

    //Player Life damage
    public void changeLives(int damage)
    {
        Life -= damage;
        lives_counter.text = "x " + Life;
    }

}
