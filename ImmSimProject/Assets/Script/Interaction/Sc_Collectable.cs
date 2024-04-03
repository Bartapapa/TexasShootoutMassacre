using UnityEngine;

public class Sc_Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Sc_CharacterMovement charMvt = other.GetComponentInParent<Sc_CharacterMovement>();
        if (charMvt)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
