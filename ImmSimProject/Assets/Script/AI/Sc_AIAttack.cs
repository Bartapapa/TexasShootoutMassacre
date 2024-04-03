using UnityEngine;

public class Sc_AIAttack : MonoBehaviour
{
    [SerializeField] private Damager _damager;
    [SerializeField] private float _attackCooldown = 1.0f;
    private float _attackTimer = 0.0f;
    private void OnTriggerStay(Collider other)
    {
        Sc_AIStateManager aiState = GetComponentInParent<Sc_AIStateManager>();
        if (!aiState) // Is Valid?
        {
            Debug.LogWarning("No AIStateManager on NPC, cannot attack");
            return;
        }
        if (_attackTimer > 0.0f || aiState.CurrentAIState != Sc_AIStateManager.AIState.FIGHT) //Can Attack and is in correct state?
        {
            return;
        }
        Damageable damageable = other.GetComponentInParent<Damageable>();
        if (damageable) //Is Valid?
        {
            _damager.DoDamage(damageable);
            _attackTimer = _attackCooldown;
        }
    }

    private void Update()
    {
        if (_attackTimer > 0.0f)
        {
            _attackTimer -= Time.deltaTime;
        }
    }
}
