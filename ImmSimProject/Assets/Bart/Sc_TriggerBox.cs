using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Sc_TriggerBox : MonoBehaviour
{
    public UnityEvent Trigger;
    [SerializeField] private bool _canBeTriggered = true;
    [SerializeField] private bool _onlyTriggerOnce = false;
    private bool _triggeredOnce = false;

    public bool CanBeTriggered
    {
        get
        {
            if (_onlyTriggerOnce && _triggeredOnce)
            {
                return false;
            }
            else
            {
                return _canBeTriggered;
            }
        }
    }

    public void SetCanBeTriggered(bool value)
    {
        _canBeTriggered = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInput player = other.transform.parent.GetComponent<PlayerInput>();
        if (!player)
        {
            Debug.Log("No player found in " + other.gameObject.name);
            return;
        }

        if (CanBeTriggered)
        {
            Trigger.Invoke();
            _canBeTriggered = false;
            _triggeredOnce = true;
            Debug.Log("Triggered!");
        }
        else
        {
            Debug.Log("Can't be triggered!");
        }
    }
}
