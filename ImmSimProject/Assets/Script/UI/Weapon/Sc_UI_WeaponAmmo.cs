using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sc_UI_WeaponAmmo : MonoBehaviour
{
    [SerializeField] Sc_Weapon _weapon;
    [SerializeField] TextMeshProUGUI _text;

    private void OnEnable()
    {
        _weapon.WeaponReloaded.RemoveListener(UpdateText);
        _weapon.WeaponReloaded.AddListener(UpdateText);

        _weapon.WeaponShot.RemoveListener(UpdateText);
        _weapon.WeaponShot.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        _weapon.WeaponReloaded.RemoveListener(UpdateText);
        _weapon.WeaponShot.RemoveListener(UpdateText);
    }
    public void UpdateText(int currentMagAmmo, int magSize)
    {
        _text.SetText(currentMagAmmo + "/" + magSize);
    }
}
