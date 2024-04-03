using UnityEngine;

[RequireComponent(typeof(Sc_AISensor))]
public class Sc_AISensorReactor : MonoBehaviour
{
    private Sc_AISensor _sensor;
    private Sc_AIStateManager _stateManager;
    private Sc_AIChaseTarget _chaseTarget;

    private void Start()
    {
        _sensor = GetComponent<Sc_AISensor>();
        _stateManager = GetComponent<Sc_AIStateManager>();
        _chaseTarget = GetComponent<Sc_AIChaseTarget>();
    }
    private void Update()
    {
        CheckSensorResult();
    }

    private GameObject CheckSensorResult()
    {
        // Check "visible" colliders
        if (_sensor.objectsFound.Count > 0)
        {
            if (_sensor.objectsFound[0].GetComponentInParent<Sc_CameraController>())
            {
                //Debug.Log("Found player! Chase them!");
                if (_stateManager.TryChangeAIState(Sc_AIStateManager.AIState.FIGHT))
                {
                    _chaseTarget._target = _sensor.objectsFound[0].transform.parent;
                }

                return _sensor.objectsFound[0];
            }
            else if (_sensor.objectsFound[0].GetComponentInParent<Sc_Bait>())
            {
                //Debug.Log("Found food! Chase them!");
                if (_stateManager.TryChangeAIState(Sc_AIStateManager.AIState.INVESTIGATING))
                {
                    _chaseTarget._target = _sensor.objectsFound[0].transform.parent;
                }

                return _sensor.objectsFound[0];
            }
        }

        //Check nearby colliders
        foreach (Collider collider in _sensor._nearbyColliders)
        {
            if (!collider)
            {
                return null; // Prevents error when reading null values
            }
            Sc_Bait bait = collider.GetComponentInParent<Sc_Bait>();
            if (bait)
            {
                if (_stateManager.TryChangeAIState(Sc_AIStateManager.AIState.INVESTIGATING))
                {
                    _chaseTarget._target = bait.transform;
                    return bait.gameObject;
                }
            }
        }
        return null;
    }
}
