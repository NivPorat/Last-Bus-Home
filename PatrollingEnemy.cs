using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public float MoveMentSpeed = 3.0f;
    public bool IsGoingRight = true;
    public float mRaycastingDistance = 1f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = IsGoingRight;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 DirectionTranslation = (IsGoingRight) ? transform.right : -transform.right;
        DirectionTranslation *= Time.deltaTime * MoveMentSpeed;
        transform.Translate(DirectionTranslation);
        CheckForWalls();
    }

    private void CheckForWalls()
    {
        Vector3 RaycastDirection = (IsGoingRight) ? Vector3.right : Vector3.left;
        // Raycasting takes as parameters a Vector3 which is the point of origin, another Vector3 which gives the direction, and finally a float for the maximum distance of the raycast
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position + RaycastDirection * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), RaycastDirection,5f);
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
