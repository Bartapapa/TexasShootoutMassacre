using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_ThrowBait : MonoBehaviour
{
    [SerializeField] private Transform _throwMuzzle;
    [SerializeField] private Rigidbody _projectile;
    [SerializeField] private float _throwStrength;
    public bool throwUnlocked;

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed && throwUnlocked)
        {
            Rigidbody projectile = Instantiate<Rigidbody>(_projectile, _throwMuzzle.position, Quaternion.identity);
            projectile.velocity = _throwMuzzle.forward * _throwStrength;
        }
    }
}
