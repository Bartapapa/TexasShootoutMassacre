using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Event_Bridge : MonoBehaviour
{
    public Animator BridgeAnimator;
    private bool _bridgeFallen = false;

    public void BridgeFall()
    {
        if (_bridgeFallen) return;

        _bridgeFallen = true;

        BridgeAnimator.Play("BridgeFall");
    }
}
