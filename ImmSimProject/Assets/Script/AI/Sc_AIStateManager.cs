using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Sc_AIStateManager : MonoBehaviour
{
    public enum AIState
    {
        DISABLED,
        PATROLLING,
        FIGHT,
        INVESTIGATING,
        KO,
        DEAD
    }

    [SerializeField] private AIState _startingAIState;
    [SerializeField] private AIState[] _statePriority;
    [SerializeField] private MonoBehaviour[] _enabledInStatePatrol;
    [SerializeField] private MonoBehaviour[] _enabledInStateFight;
    [SerializeField] private MonoBehaviour[] _enabledInStateInvestigating;
    [SerializeField] private MonoBehaviour[] _enabledInStateKO;
    [SerializeField] private MonoBehaviour[] _enabledInStateDead;

    private AIState _currentAIState;

    public AIState CurrentAIState => _currentAIState;

    public UnityEvent<AIState> AIStateChanged;
    public void ChangeAIState(AIState newAIState)
    {
        _currentAIState = newAIState;
        switch (_currentAIState)
        {
            case AIState.DISABLED:
                DisableAll();
                break;
            case AIState.PATROLLING:
                DisableAll();
                foreach (MonoBehaviour script in _enabledInStatePatrol)
                {
                    script.enabled = true;
                }
                break;
            case AIState.FIGHT:
                DisableAll();
                foreach (MonoBehaviour script in _enabledInStateFight)
                {
                    script.enabled = true;
                }
                break;
            case AIState.INVESTIGATING:
                DisableAll();
                foreach (MonoBehaviour script in _enabledInStateInvestigating)
                {
                    script.enabled = true;
                }
                break;
            case AIState.KO:
                DisableAll();
                foreach (MonoBehaviour script in _enabledInStateKO)
                {
                    script.enabled = true;
                }
                break;
            case AIState.DEAD:
                DisableAll();
                foreach (MonoBehaviour script in _enabledInStateDead)
                {
                    script.enabled = true;
                }
                break;

            default:
                break;
        }
        AIStateChanged.Invoke(_currentAIState);
    }

    private void DisableAll()
    {
        foreach (MonoBehaviour script in _enabledInStatePatrol)
        {
            script.enabled = false;
        }
        foreach (MonoBehaviour script in _enabledInStateFight)
        {
            script.enabled = false;
        }
        foreach (MonoBehaviour script in _enabledInStateInvestigating)
        {
            script.enabled = false;
        }
        foreach (MonoBehaviour script in _enabledInStateKO)
        {
            script.enabled = false;
        }
        foreach (MonoBehaviour script in _enabledInStateDead)
        {
            script.enabled = false;
        }
    }

    public bool TryChangeAIState(AIState newAIState)
    {
        int currentStateIndex = Array.IndexOf(_statePriority, _currentAIState);
        int newStateIndex = Array.IndexOf(_statePriority, newAIState);
        if (newStateIndex < currentStateIndex)
        {
            ChangeAIState(newAIState);
            return true;
        }
        return false;
    }
    private void Start()
    {
        ChangeAIState(_startingAIState);
    }
}
