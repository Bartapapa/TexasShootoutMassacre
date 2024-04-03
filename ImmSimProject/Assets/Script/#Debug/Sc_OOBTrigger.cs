using UnityEngine;
using UnityEngine.Events;

public class Sc_OOBTrigger : MonoBehaviour
{
    public UnityEvent<Collider> OOBTriggered;
    private void OnTriggerEnter(Collider other)
    {
        OOBTriggered.Invoke(other);
    }
}
