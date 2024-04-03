using UnityEngine;
using TMPro;

public class Sc_UI_HPDisplayer : MonoBehaviour
{

    [SerializeField] private Damageable _damageable;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _damageable.OnDamageTaken.RemoveListener(OnHPChanged);
        _damageable.OnDamageTaken.AddListener(OnHPChanged);
    }

    private void OnDisable()
    {
        _damageable.OnDamageTaken.RemoveListener(OnHPChanged);
    }
    private void Start()
    {
        UpdateHP(_damageable.CurrentHealth, _damageable.MaxHP);
    }

    public void OnHPChanged(float curHP, float maxHP, Damager damager)
    {
        UpdateHP(curHP, maxHP);
    }
    public void UpdateHP(float curHP, float maxHP)
    {
        _text.text = ("HP: " + curHP + "/" + maxHP);
    }
}
