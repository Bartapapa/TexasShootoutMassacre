//By ALBERT Esteban
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private bool _destroyOnDeath = true;
    [SerializeField] private float _health = 100;

    public UnityEvent<float, float, Damager> OnDamageTaken;
    public UnityEvent<Damageable> Died;

    public int MaxHP => _maxHealth;
    public float CurrentHealth => _health;
    public bool DestroyOnDeath => _destroyOnDeath;

    public void SetDestroyOnDeath(bool destroyOnDeath)
    {
        _destroyOnDeath = destroyOnDeath;
    }

    public void setMaxHp(float maxHp, bool shouldRestoreLife, bool shouldKeepPercent)
    {
        float healthPercentage = _health / _maxHealth;
        _maxHealth = (int)maxHp;
        if (shouldRestoreLife)
        {
            _health = _maxHealth;
        }
        else if (shouldKeepPercent)
        {
            _health = _maxHealth * healthPercentage;
        }
    }

    public void TakeDamage(float damage, out float health, Damager damager)
    {
        _health -= damage;
        health = _health;
        OnDamageTaken.Invoke(_health, _maxHealth, damager);

        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Died.Invoke(this);
        if (_destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
