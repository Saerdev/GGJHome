using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    [HideInInspector]
    public FlockingManager flockingManager;

    Vector3 moveDir, randomPos;

    float noiseOffset, velocity;

    // Start is called before the first frame update
    void Start()
    {
        noiseOffset = Random.value * 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = GetTargetDirection();

        // Face target movement direction
        Quaternion targetRotation = Quaternion.LookRotation(moveDir, transform.up);
        if (transform.rotation != targetRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, flockingManager.rotationSpeed);

        // Randomize velocity
        float noise = Mathf.PerlinNoise(Time.time, noiseOffset) * 2.0f - 1.0f;
        velocity = flockingManager.predatorSpeed * (1.0f + noise * flockingManager.speedVariation);
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    Vector3 GetTargetDirection()
    {
        if (Random.Range(0, 1000) < 10)
        {
            randomPos = Random.insideUnitSphere * flockingManager.worldSize;
        }

        return (randomPos - transform.position).normalized;
    }
}
