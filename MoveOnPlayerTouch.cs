using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPlayerTouch :oscillator
{
    public bool IsPlayerOn = false;    

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerOn)
            Oscillate();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            {

                collision.collider.transform.SetParent(transform);
            IsPlayerOn = true;
            }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.collider.transform.SetParent(transform);
            IsPlayerOn = false;
        }
    }
}
