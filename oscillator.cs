using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 0f); //IN WHICH DIRECTION TO MOVE
    [SerializeField] float period = 5f;
    float MovementFactor; //RANGE OF HOW MUCH TO MOVE IT
    Vector3 StartingPos;//STARTING POSITION OF OBJECT

    // Start is called before the first frame update
    void Start()
    {
        StartingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    protected void Oscillate()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f; // ~6.28 = 3.14*2 for size in radians for sine movement

        float rawSineCycles = Mathf.Sin(cycles * tau);
        MovementFactor = rawSineCycles / 2f + 0.5f;

        Vector3 Offset = movementVector * MovementFactor;
        transform.position = StartingPos + Offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.collider.transform.SetParent(null);
        }
    }
}
