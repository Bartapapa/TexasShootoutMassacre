using UnityEngine;
using UnityEngine.Events;

public class Sc_OOBTeleporter : MonoBehaviour
{
    [SerializeField] private Sc_OOBTrigger[] _deathBoxes;

    private void OnEnable()
    {
        foreach (Sc_OOBTrigger oobTrigger in _deathBoxes)
        {
            oobTrigger.OOBTriggered.RemoveListener(TeleportPlayer);
            oobTrigger.OOBTriggered.AddListener(TeleportPlayer);
        }
    }

    private void OnDisable()
    {
        foreach (Sc_OOBTrigger oobTrigger in _deathBoxes)
        {
            oobTrigger.OOBTriggered.RemoveListener(TeleportPlayer);
        }
    }

    private void TeleportPlayer(Collider collider)
    {
        Sc_CharacterMovement characterMvt = collider.GetComponentInParent<Sc_CharacterMovement>();
        if (characterMvt)
        {
            Rigidbody rigidbody = collider.GetComponentInParent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.Move(transform.position, transform.rotation);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2.0f);
    }
}
