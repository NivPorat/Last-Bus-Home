using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float jumpHeight = 10f;
    [SerializeField] public float jumpLimit = 5f;

    float levelLoadDelay = 2f;
    public float Weight = 9.8f;
    Rigidbody2D rigidBody;
    public Animator animator;
    public LivesManager LM;
    public scoreManager scoreAccumulator;
    public bool isOnGround = true;
    private int Level1Score = 25;

    enum State { Alive, Dead, trancending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Move();
            Jump();
        }
    }
    void Move()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float moveBy = horizontalMove * speed;
        Vector3 characterScale = transform.localScale;
        rigidBody.velocity = new Vector2(moveBy, rigidBody.velocity.y);
        animator.SetFloat("Speed",Mathf.Abs( horizontalMove));
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);

        }


        else
        {
            rigidBody.AddForce(-Vector2.up * jumpHeight * Time.deltaTime, ForceMode2D.Impulse);
            Move();

        }
        if (transform.position.y >= jumpLimit)//set limit cap on how high can player jump
        {
            
            Move();
            rigidBody.velocity = Vector2.zero;
            Vector2 vel = rigidBody.velocity;
            vel.y -= Weight * Time.deltaTime;
            rigidBody.velocity = vel;
        }
    }
    //these functions are for not double jumping, because of floor collision
    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
            animator.SetBool("IsJumping", false);
        }
     }
    private void OnCollisionExit2D(Collision2D Col)
    {
        if (Col.gameObject.CompareTag("ground"))
        {
            isOnGround = false;
            animator.SetBool("IsJumping", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D Col)//this is for destroying coin when triggered by player
    {
        if (Col.gameObject.CompareTag("coin"))
        {
            scoreManager.instance.changeScore();///!!!!!!!
            Destroy(Col.gameObject);
            
        }
            
    }



    private void OnDeath()//on death - stop sound, play death sound, return to level 1
    { if(LM.Life == 0)
        state = State.Dead;
        Invoke("LoadOnDeath", levelLoadDelay);
    }

    private void LoadOnDeath()
    {
        SceneManager.LoadScene(0);//change this when you add multiple scenes!
    }
    private void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
            nextLevelIndex = 0;
        if(scoreAccumulator.score == Level1Score)
        SceneManager.LoadScene(nextLevelIndex);
    }//load next level..duh
}
  /*private void MoveRight()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        //rigidBody.velocity = new Vector2(-moveSpeed * 100 * Time.deltaTime, 0);
        {
            float moveBy = x * speed;
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
        }
    }

    private void MoveLeft()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        //  rigidBody.velocity = new Vector2(moveSpeed * 100 * Time.deltaTime, 0);
        {
            float moveBy = x * speed;
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
        }
    }*/