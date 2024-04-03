using UnityEngine;

public class Sc_DamageableStateReaction : MonoBehaviour
{
    private void OnEnable()
    {
        Damageable damageable = gameObject.GetComponent<Damageable>();
        damageable.OnDamageTaken.RemoveListener(OnDamageTaken);
        damageable.OnDamageTaken.AddListener(OnDamageTaken);
    }
    private void OnDisable()
    {
        gameObject.GetComponent<Damageable>().OnDamageTaken.RemoveListener(OnDamageTaken);
    }


    public void OnDamageTaken(float curHP, float maxHP, Damager damager)
    {
        Sc_AIStateManager stateManager = gameObject.GetComponent<Sc_AIStateManager>();
        if (stateManager)
        {
            if (stateManager.TryChangeAIState(Sc_AIStateManager.AIState.FIGHT))
            {
                Sc_AIChaseTarget chaseTarget = GetComponent<Sc_AIChaseTarget>();
                if (chaseTarget)
                {
                    chaseTarget._target = damager.transform.parent;
                }
            }
        }
    }
}
