using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public GameObject agentPrefab, predatorPrefab;
    public int flockSize;
    public float spawnRadius = 30;
    public Transform target;

    [Space(10)]
    public bool enableChaseTarget;
    public float targetChangeFrequency = .05f;
    public float worldSize = 70f;

    [Range(0, 100)]
    public float agentSpeed;
    [Range(0, 3)]
    public float separation = 1, alignment = 1, cohesion = 1;
    [Range(0, 50)]
    public float neighborRadius;

    [Range(0, .2f)]
    public float rotationSpeed = 0.07f;
    [Range(0, 1)]
    public float speedVariation = 0.5f;

    public LayerMask agentMask;

    [HideInInspector]
    public Vector3 flockCenter;

    [Space(10)]
    public bool avoidPredators;
    public float predatorAvoidDistance = 3;
    public Vector3 mousePos;
    public float predatorSpeed;
    public LayerMask predatorMask;

    private List<Agent> agents = new List<Agent>();
    private List<Predator> predators = new List<Predator>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            Destroy(agents[i].gameObject);
        }

        for (int i = 0; i < flockSize; i++)
        {
            CreateAgent();
        }
    }

    private void Update()
    {
        if (Random.Range(0, 1000) < targetChangeFrequency * 100)
        {
            target.transform.position = Random.insideUnitSphere * worldSize;
        }

        mousePos = Input.mousePosition;
        mousePos.z = 15;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateAgent(mousePos, Color.white);
            flockSize = agents.Count;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            CreatePredator(mousePos);
        }

        flockCenter = Vector3.zero;
        for (int i = 0; i < agents.Count; i++)
        {
            flockCenter += agents[i].transform.position;
        }

        flockCenter /= agents.Count;
    }

    private void CreateAgent()
    {
        Vector3 position = target.position + Random.onUnitSphere * spawnRadius;
        CreateAgent(position, Color.white);
    }

    private void CreateAgent(Vector3 position, Color color)
    {
        GameObject agent = Instantiate(agentPrefab, position, Random.rotation);
        agent.transform.SetParent(transform);
        //agent.GetComponent<Renderer>().material.color = color;
        Agent newAgent = agent.GetComponent<Agent>();
        newAgent.flockingManager = this;
        agents.Add(newAgent);
    }

    private void CreatePredator(Vector3 position)
    {
        GameObject predator = Instantiate(predatorPrefab, position, Random.rotation);
        //predator.GetComponent<Renderer>().material.color = Color.red;
        Predator newPredator = predator.GetComponent<Predator>();
        newPredator.flockingManager = this;
        predators.Add(newPredator);
    }
}
