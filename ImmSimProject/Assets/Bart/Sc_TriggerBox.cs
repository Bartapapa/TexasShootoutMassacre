using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        set
        {
            _canBeTriggered = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanBeTriggered)
        {
            Trigger.Invoke();
            _canBeTriggered = false;
            _triggeredOnce = true;
        }
    }
}
