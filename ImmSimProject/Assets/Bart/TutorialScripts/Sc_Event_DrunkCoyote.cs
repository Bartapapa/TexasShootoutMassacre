using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sc_Event_DrunkCoyote : MonoBehaviour
{
    public UnityEvent CoyoteDeath;
    public UnityEvent CoyoteFear;

    private bool _ended = false;

    public void OnBottleShot()
    {
        if (_ended) return;

        CoyoteFear?.Invoke();
        _ended = true;
    }

    public void OnCoyoteDeath()
    {
        if (_ended) return;

        CoyoteDeath?.Invoke();
        _ended = true;
    }
}
