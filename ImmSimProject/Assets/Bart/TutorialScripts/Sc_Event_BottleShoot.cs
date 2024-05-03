using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sc_Event_BottleShoot : MonoBehaviour
{
    public Sc_Weapon _playerWeaponRef;

    private bool _started = false;
    private bool _ended = false;
    private int _shotsFired = 0;
    private int _bottlesShot = 0;

    public UnityEvent EventStart;
    public UnityEvent EventEnd;
    public UnityEvent EndPoor;
    public UnityEvent EndMid;
    public UnityEvent EndGreat;

    private void OnEnable()
    {
        _playerWeaponRef.ShootWeapon -= AddShotFired;
        _playerWeaponRef.ShootWeapon += AddShotFired;
    }

    private void OnDisable()
    {
        _playerWeaponRef.ShootWeapon -= AddShotFired;
    }

    public void StartEvent()
    {
        _started = true;
        _ended = false;

        _shotsFired = 0;
        _bottlesShot = 0;

        EventStart?.Invoke();
        
    }

    public void EndEvent()
    {
        _started = false;
        _ended = true;

        Debug.LogWarning(_bottlesShot);

        EventEnd?.Invoke();
        if (_bottlesShot < 2)
        {
            EndPoor?.Invoke();
        }
        else if (_bottlesShot >=2 && _bottlesShot < 5)
        {
            EndMid?.Invoke();
        }
        else
        {
            EndGreat?.Invoke();
        }
    }

    public void AddShotFired()
    {
        if (!_started || _ended) return;
        _shotsFired++;

        if (_shotsFired >= 6)
        {
            EndEvent();
        }
    }

    public void AddBottleShot()
    {
        if (!_started && !_ended)
        {
            StartEvent();
            AddShotFired();
        }

        _bottlesShot++;
    }
}
