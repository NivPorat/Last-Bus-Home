using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// This Class manages collision between player and enemies, obstacles etc
/// and is responsible of Player Lives Decrease
/// </summary>
public class deathCollision : MonoBehaviour
{
    int scene;//index of current level
    public int LivesDown = 1;//damage to player
    public GameObject Player;//reference of player object
    public LivesManager LifeManager;//reference of lives manager object

    // Start is called before the first frame update
    private void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;//index of current scene
    }

    //Check collision enter to player's collison to get damage
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))//give hit damage to player on colision enter
        {
            LifeManager.changeLives(LivesDown);
        }
        
    }

    
}
