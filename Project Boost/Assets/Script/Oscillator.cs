using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;
    [SerializeField] float period;

    void Start()
    {
        startingPosition= transform.position;
    }


    void Update()
    {
        float cyles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cyles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 ofset = movementVector * movementFactor;
        transform.position = startingPosition + ofset;
    }
}
