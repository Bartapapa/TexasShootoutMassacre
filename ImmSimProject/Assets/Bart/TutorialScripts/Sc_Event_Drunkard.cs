using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sc_Event_Drunkard : MonoBehaviour
{
    public UnityEvent DrunkardDeath;
    public UnityEvent DrunkardFear;

    private bool _ended = false;

    public void OnBottleShot()
    {
        if (_ended) return;

        DrunkardFear?.Invoke();
        _ended = true;
    }

    public void OnDrunkardDeath()
    {
        if (_ended) return;

        DrunkardDeath?.Invoke();
        _ended = true;
    }
}
