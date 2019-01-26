using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Separation, Alignment, Cohesion
public class Agent : MonoBehaviour
{
    [HideInInspector]
    public FlockingManager flockingManager;
    [HideInInspector]
    public Vector3 target;
    public int neighborCount;

    public Vector3 separation, alignment, cohesion;

    private Collider[] neighbors, predators;
    private Vector3 moveDir;

    float noiseOffset, velocity;

    Vector3 predatorPos;

    private void Start()
    {
        noiseOffset = Random.value * 10.0f; 
    }

    private void Update()
    {
        neighbors = Physics.OverlapSphere(transform.position, flockingManager.neighborRadius, flockingManager.agentMask);
        Vector3 avgNeighborPos = Vector3.zero;
        separation = Vector3.zero;
        alignment = transform.forward;
        cohesion = Vector3.zero;

        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i].gameObject == gameObject)
                continue;
            
            // Separation
            separation += GetSeparationVector(neighbors[i].transform.position);

            // Alignment
            alignment += neighbors[i].transform.forward;

            // Cohesion
            cohesion += neighbors[i].transform.position;
        }

        float avg = 0;
        if (neighbors.Length > 0)
        {
            avg = 1.0f / (neighbors.Length);
            neighborCount = neighbors.Length;
        }

        alignment *= avg * (flockingManager.alignment);
        cohesion *= avg;
        cohesion = cohesion * flockingManager.cohesion;
        moveDir = cohesion + alignment + separation + GetTargetDirection() + AvoidPredator();

        // Used if not giving a bias for agents to return to the origin
        //if (moveDir == transform.forward)
        //    moveDir = transform.forward + transform.position;

        // Face target movement direction
        Quaternion targetRotation = transform.rotation;
        targetRotation = Quaternion.LookRotation(moveDir - transform.position, transform.up);
        if (transform.rotation != targetRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, flockingManager.rotationSpeed);

        // Randomize velocity
        float noise = Mathf.PerlinNoise(Time.time, noiseOffset) * 2.0f - 1.0f;
        velocity = flockingManager.agentSpeed * (1.0f + noise * flockingManager.speedVariation);
        transform.position += transform.forward * velocity * PredatorProximity() * Time.deltaTime;
    }

    Vector3 GetSeparationVector(Vector3 neighborPos)
    {
        Vector3 diff = transform.position - neighborPos;
        float diffMag = diff.magnitude;
        if (diffMag < flockingManager.separation)
            return diff;
        else
            return Vector3.zero;
    }

    Vector3 GetTargetDirection()
    {
        if (flockingManager.enableRandomTarget)
            return flockingManager.target.transform.position - transform.position;
        else
            return Vector3.zero;
    }

    Vector3 AvoidPredator()
    {
        if (flockingManager.avoidPredators)
        {
            predators = Physics.OverlapSphere(transform.position, 10, flockingManager.predatorMask);
            Vector3 avoidDir = Vector3.zero;
            for (int i = 0; i < predators.Length; i++)
            {
                avoidDir += predators[i].transform.position;
            }

            if (predators.Length > 0)
            {
                avoidDir /= predators.Length;
                return transform.position - avoidDir;
            }
            else
                return Vector3.zero;
        }
        else
            return Vector3.zero;
    }

    float PredatorProximity()
    {
        float scaler = 1, minSpeed = 1, maxSpeed = 2, distance;

        if (flockingManager.avoidPredators)
        {
            if (predators.Length == 0)
                return scaler;

            distance = Vector3.Distance(transform.position, predatorPos);

            //if (distance < flockingManager.predatorAvoidDistance)
            //    scaler = 3;

            scaler = 1 / (distance / flockingManager.predatorAvoidDistance);
            scaler = Mathf.Clamp(distance, minSpeed, maxSpeed);
        }

        return scaler;
    }
}
