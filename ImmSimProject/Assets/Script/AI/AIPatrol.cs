using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       //Mandatory library for using AI scripts

public class AIPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float _delayBetweenPatrol;
    private int destinationPoint = 0;
    private float _delayTimer = 0.0f;
    private NavMeshAgent myNavMeshAgent;

    private void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();

        //Disabling auto-braking allows for continuous movement between points
        //( ie, the agent doesn't slow down as it approaches a destination point )
        myNavMeshAgent.autoBraking = false;


    }


    // Update is called once per frame
    void Update()
    {
        //Choose the next destination point when the agent gets close to the current one
        if (!myNavMeshAgent.pathPending && myNavMeshAgent.remainingDistance <= 0.5f)
        {
            if (_delayTimer <= 0.0f)
            {
                GoToNextPoint();
                _delayTimer = _delayBetweenPatrol;
            }
            _delayTimer -= Time.deltaTime;
        }
    }

    private void GoToNextPoint()
    {
        //Returns if no points have been set up
        if (waypoints.Length == 0)
        {
            return;
        }

        //Set the agent to go to the currently selected destination
        myNavMeshAgent.destination = waypoints[destinationPoint].position;

        //Cycling to the start if necessary
        destinationPoint = (destinationPoint + 1) % waypoints.Length;
    }
}
