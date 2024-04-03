using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_WeaponController : MonoBehaviour
{
    [SerializeField] private Sc_Weapon _weaponControlled;

    public void WeaponTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _weaponControlled.Shoot();
        }
    }

    public void ReloadTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _weaponControlled.ReloadStart();
        }
    }
}
