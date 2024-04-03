using UnityEngine;

public class Sc_NPC_LifetimeEnabler : MonoBehaviour
{
    [SerializeField] private Sc_NPC_Lifetime _npcLifetime;
    private bool _hasBeenTriggered = false;

    private void Start()
    {
        _npcLifetime.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        Sc_CharacterMovement characterMvt = other.GetComponentInParent<Sc_CharacterMovement>();
        if (characterMvt && _hasBeenTriggered == false)
        {
            _npcLifetime.enabled = true;
            _hasBeenTriggered = true;
        }
    }
}
