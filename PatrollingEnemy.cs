using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// objects using this script move between a determined range
/// as calibrated in Unity
/// to similate Patrol
/// </summary>
public class PatrollingEnemy : MonoBehaviour
{
    public float MoveMentSpeed = 3.0f;//object movement speed
    public bool IsGoingRight = true;//object facing
    public float mRaycastingDistance = 1f;//lenght of checking ray
    private SpriteRenderer spriteRenderer;//images of object

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();//get reference of object renderer
        spriteRenderer.flipX = IsGoingRight;//object facing right at start
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 DirectionTranslation = (IsGoingRight) ? transform.right : -transform.right;//check object direction
        DirectionTranslation *= Time.deltaTime * MoveMentSpeed;//conversion to frames
        transform.Translate(DirectionTranslation);//object movement
        CheckForWalls();
    }

    //check for stoppers of object
    private void CheckForWalls()
    {
        Vector3 RaycastDirection = (IsGoingRight) ? Vector3.right : Vector3.left;
        // Raycasting takes as parameters a Vector3 which is the point of origin, another Vector3 which gives the direction, and finally a float for the maximum distance of the raycast
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position + RaycastDirection * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), RaycastDirection,1f);
        if(raycastHit.collider != null)
        {
            if(raycastHit.transform.tag == "ground" || raycastHit.transform.tag == "Stopper" )
            {
                IsGoingRight = !IsGoingRight;
                spriteRenderer.flipX = IsGoingRight;
            }
        }
    }
}
