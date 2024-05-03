using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Sc_Event_AloneGuyAndHome : MonoBehaviour
{
    private int _coyotesKilled = 0;

    public UnityEvent CoyotesKilled;
    public UnityEvent RopeShot;

    private bool _ended = false;

    public void ShootRope()
    {
        if (_ended) return;

        _ended = true;
        RopeShot?.Invoke();
    }

    public void KillCoyotes()
    {
        if (_ended) return;

        _ended = true;
        CoyotesKilled?.Invoke();
    }

    public void IncrementCoyotesKilled()
    {
        _coyotesKilled++;
        if (_coyotesKilled >= 4)
        {
            KillCoyotes();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("BlockoutLevelFinaleV1");
    }
}
