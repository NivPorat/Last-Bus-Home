using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// main Controller class
/// controlling player movements, physics, calculations
/// and collisions with different game objects
/// </summary>
public class playercontroller : MonoBehaviour
{
    //Player stats for movement in the world
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float jumpHeight = 10f;
    [SerializeField] public float jumpLimit = 5f;

    float horizontalMove = 0f;//player direction movement on axis

    //References of player components
    public Rigidbody2D rigidBody;
    public Animator animator;
    public scoreManager scoreAccumulator;
    public SpriteRenderer Renderer;
    private int Level;

    //start loads once - on first scene load
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();//gets reference to player graphic renderer
        rigidBody = GetComponent<Rigidbody2D>();
        Level = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Jump();
        Move();
        //this method is for developing an easy passage between scenes when playing
        DeveloperKeysInput();
        CheckLimit();

    }

    //this method is for developing an easy passage between scenes when playing
    private void DeveloperKeysInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene(Level + 1);
        if (Input.GetKeyDown(KeyCode.K))
            SceneManager.LoadScene(Level - 1);
    }

    //player movement by keyboard input
    void Move()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;//gets input as horizontal axis - as defined by Unity

        if (horizontalMove > 0)//moving in right direction ->
        {
            if (animator.GetBool("isOnGround") && !animator.GetBool("IsJumping"))
                animator.SetBool("isRuning", true);
            else
                animator.SetBool("isRuning", false);
            Renderer.flipX = false;
            Vector3 characterScale = transform.localScale;
            rigidBody.velocity = new Vector2(horizontalMove, rigidBody.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        }
        else if (horizontalMove < 0)//moving in left direction <-
        {
            if (animator.GetBool("isOnGround") && !animator.GetBool("IsJumping"))
                animator.SetBool("isRuning", true);
            else
                animator.SetBool("isRuning", false);
            Vector3 characterScale = transform.localScale;
            rigidBody.velocity = new Vector2(horizontalMove, rigidBody.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            Renderer.flipX = true;

        }
        else//player is standing in place |
        {
            rigidBody.velocity = new Vector2(horizontalMove, rigidBody.velocity.y);

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            animator.SetBool("isRuning", false);
        }
    }
    //player jump by keyboard input
    private void Jump()
    {
        
        if (Input.GetKey(KeyCode.Space) && animator.GetBool("isOnGround"))
        {
            rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);//!!!Need to check ForceMode
            animator.SetBool("IsJumping", true);
            animator.SetBool("isOnGround", false);
            //Move();
            
        }

    }

    //set limit on player jump height
    private void CheckLimit()
    {
        if (transform.position.y >= jumpLimit && !animator.GetBool("isOnGround"))//set limit cap on how high can player jump
        {
            //Move();//indicating that player can move in 2d while jumping
            Vector2 vel = rigidBody.velocity;// this var saves the velocity of player while jumping
            vel.y -= rigidBody.mass * Time.deltaTime;
            rigidBody.velocity = vel;
        }
    }
   //physics with collision between player and enemy
    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.CompareTag("Enemy"))
        {
            rigidBody.AddForce(new Vector2(-1, 0));//push physics
        }
    }

    // for not double jumping, because of floor collision
    void OnCollisionStay2D(Collision2D Col) 
    {
        if (Col.gameObject.CompareTag("ground"))
        {
            animator.SetBool("isOnGround", true);
            animator.SetBool("IsJumping", false);
        }
    }

    //Check if out ground
    void OnCollisionExit2D(Collision2D Col)
    {
        if (!(Col.gameObject.CompareTag("ground")))
        {
            animator.SetBool("isOnGround", false);
        }
        if (!(Col.gameObject.CompareTag("Enemy")))
        {
            animator.SetBool("isOnGround", true);
        }
    }

    //getting coin object and destroy him
    void OnTriggerEnter2D(Collider2D Col)//this is for destroying coin when triggered by player
    {
        if (Col.gameObject.CompareTag("coin"))
        {
            scoreManager.score_manager.changeScore();
            Destroy(Col.gameObject);
        }
    }

}