using UnityEngine;
using UnityEngine.AI;

public class Sc_AIChaseTarget : MonoBehaviour
{
    [SerializeField] private float _delayAfterChase = 0.2f;
    [SerializeField] private float _chaseDistanceThreshold = 0.5f;
    [SerializeField] public Transform _target;
    private float _delayTimer = 0.0f;
    private NavMeshAgent _myNavMeshAgent;
    private bool isInDelay = false;

    private void Start()
    {
        _myNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!_target)
        {
            return;
        }

        if (isInDelay)
        {
            _delayTimer -= Time.deltaTime;
            if (_delayTimer > 0.0f)
            {
                return;
            }
            else
            {
                isInDelay = false;
            }
        }

        _myNavMeshAgent.destination = _target.position;
        if (!_myNavMeshAgent.pathPending && _myNavMeshAgent.remainingDistance <= _chaseDistanceThreshold)
        {
            isInDelay = true;
            _delayTimer = _delayAfterChase;
        }
    }
}
