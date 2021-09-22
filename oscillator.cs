using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// class uses synosodial calculations
/// to move platform on different axis
/// </summary>
[DisallowMultipleComponent]
public class oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 0f); //IN WHICH DIRECTION TO MOVE
    [SerializeField] float period = 5f;//time of movement of platform from place to place
    float MovementFactor; //RANGE OF HOW MUCH TO MOVE IT
    Vector3 StartingPos;//STARTING POSITION OF OBJECT

    void Start()
    {
        StartingPos = transform.position;
    }

    
    void Update()
    {
        Oscillate();
    }

    //Calculation of movements of platform on axis by sinus
    public void Oscillate()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f; // ~6.28 = 3.14*2 for size in radians for sine movement

        float rawSineCycles = Mathf.Sin(cycles * tau);
        MovementFactor = rawSineCycles / 2f + 0.5f;

        Vector3 Offset = movementVector * MovementFactor;
        transform.position = StartingPos + Offset;
    }

}
