using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class TutorAIController : NetworkBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private NavPathHolder navPathHolder;

    private Queue<Vector3> pathQueue;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        pathQueue = new Queue<Vector3>(navPathHolder.GetPath());
    }

    private void Start()
    {
        if (NetworkManager.Singleton.IsClient)
            Destroy(navMeshAgent);
    }

    private void Update()
    {
        if (IsServer)
        {
            if (!navMeshAgent.hasPath)
            {
                Vector3 point = pathQueue.Dequeue();
                navMeshAgent.SetDestination(point);
                pathQueue.Enqueue(point);
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            if (navMeshAgent.desiredVelocity.sqrMagnitude > 0.1f)
            {
                Vector3 forward = transform.forward;
                forward = Vector3.Slerp(forward, navMeshAgent.desiredVelocity.normalized, 0.5f);
                transform.LookAt(transform.position + forward, Vector3.up);
            }
        }
    }
}
