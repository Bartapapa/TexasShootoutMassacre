//By ALBERT Esteban
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _attack = 1;


    public UnityEvent<Damageable> DamageDone;
    
    public void SetDamage(float damage)
    {
        _attack = damage;
    }
    public float getDamage
    {
        get { return _attack; }
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable otherDamageable = other.GetComponent<Damageable>();

        if (otherDamageable != null)
        {
           
        }
    }

    public void DoDamage(Damageable damageable, float damage)
    {
        damageable.TakeDamage(damage, out float health, this);
        DamageDone.Invoke(damageable);
    }

    public void DoDamage(Damageable damageable)
    {
        damageable.TakeDamage(_attack, out float health, this);
        DamageDone.Invoke(damageable);
    }
}
