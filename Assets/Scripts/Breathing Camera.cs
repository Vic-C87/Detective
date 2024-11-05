using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingEffect : MonoBehaviour
{
    // The amount of time it takes for one breath cycle (in seconds)
    public float breathCycleDuration = 5.0f;

    // The maximum amount that the camera will move and rotate during a breath cycle
    public float maxMovementAmplitude = 0.1f;
    public float maxRotationAmplitude = 1.0f;

    // The current position and rotation offsets of the camera
    private Vector3 movementOffset = Vector3.zero;
    private Vector3 rotationOffset = Vector3.zero;

    // The random values that will be used to slightly offset the movement and rotation of the camera
    private float movementRandomValue = 0.0f;
    private float rotationRandomValue = 0.0f;

    void Start()
    {
        // Generate random values for the movement and rotation offsets
        movementRandomValue = Random.Range(-1.0f, 1.0f);
        rotationRandomValue = Random.Range(-1.0f, 1.0f);
    }

    void Update()
    {
        // Calculate the current phase of the breath cycle (from 0 to 1)
        float t = Time.time / breathCycleDuration;
        t = t - Mathf.Floor(t);

        // Calculate the sinusoidal curve for the movement and rotation offsets
        movementOffset.x = Mathf.Sin(t * Mathf.PI * 2) * maxMovementAmplitude * movementRandomValue;
        movementOffset.y = Mathf.Sin(t * Mathf.PI * 2) * maxMovementAmplitude * movementRandomValue;
        movementOffset.z = Mathf.Sin(t * Mathf.PI * 2) * maxMovementAmplitude * movementRandomValue;
        rotationOffset.x = Mathf.Sin(t * Mathf.PI * 2) * maxRotationAmplitude * rotationRandomValue;
        rotationOffset.y = Mathf.Sin(t * Mathf.PI * 2) * maxRotationAmplitude * rotationRandomValue;
        rotationOffset.z = Mathf.Sin(t * Mathf.PI * 2) * maxRotationAmplitude * rotationRandomValue;

        // Apply the movement and rotation offsets to the camera
        transform.position += movementOffset;
        transform.eulerAngles += rotationOffset;
    }
}