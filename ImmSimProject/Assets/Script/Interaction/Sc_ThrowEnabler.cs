using UnityEngine;

public class Sc_ThrowEnabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Sc_ThrowBait throwBait = other.GetComponentInParent<Sc_ThrowBait>();
        if (throwBait)
        {
            throwBait.throwUnlocked = true;
        }
    }
}
