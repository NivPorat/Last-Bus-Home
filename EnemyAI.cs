using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// Objects using this class actively search the player
/// follow him around for their range of detection
/// and change movement speed accordingly
/// </summary>
public class EnemyAI : MonoBehaviour
{

    private Transform player;//player reference position
    public float distance;//distance between player and enemy
    public float Speed = 0.1f;//enemy speed
    public static bool isPlayerAlive = true;//playe lives>0
    public bool isChasing;//chasimg enemy flag
    public Animator animator;//Gordo animator
    public LivesManager livesManager;//Lives manager reference
    public bool RightDirection = false;//enemy facing to right flag


    // Use this for initialization
    void Start()
    {
        isChasing = false;//chasing flag
        animator.SetTrigger("Idle_01");//defualt idle animatioin
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();//player reference
    }

    //Changing facing direction of enemy by axis X
    public void flip()
    {
        RightDirection = !RightDirection;
        float new_x = -animator.transform.localScale.x;//new position X,x value indicating direction
        animator.transform.localScale = new Vector3(new_x, transform.localScale.y, 0);//new animation with current Y of enemy in game space

    }

    void Update()
    {
        //if player still alive
        if (livesManager.Life > 0)
        {
            distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);//count distance between player and enemy
            if ((player.transform.position.x > transform.position.x) && !RightDirection)//if player from right turn enemy to right
                flip();
            if ((player.transform.position.x < transform.position.x) && RightDirection)//if player from left turn enemy to left
                flip();
            //chasing distance
            if (distance < 7f)
            {
                if (!animator.GetBool("isChasing") && animator.GetBool("isHi") == false)
                {
                    animator.SetBool("isChasing", true);
                }
                //when player to close use Hi animation
                if (distance < 2.8f && animator.GetBool("isHi") == false)
                {
                    animator.SetBool("isChasing", false);
                    animator.SetBool("isHi", true);
                }
                //if still in chaising range but not in range of Hi animation continue to chase
                else if(distance >2.8f )
                {
                    animator.SetBool("isHi", false);
                    animator.SetBool("isChasing", true);
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), Speed * Time.deltaTime);//chasing only by axis X
                }
                
                
            }
            //if not in range of chasing
            else if(animator.GetBool("isChasing") || animator.GetBool("isHi"))
            {
                //animator.ResetTrigger("Hi_01");
                animator.SetBool("isHi", false);
                animator.SetBool("isChasing", false);
            }

        }
    }
}

