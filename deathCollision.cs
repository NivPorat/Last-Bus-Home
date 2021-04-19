using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public int LivesDown = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            LivesManager.instance.changeLives(LivesDown);
            Debug.Log("hit");
        }
    }
}
