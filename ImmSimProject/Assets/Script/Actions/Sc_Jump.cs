using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Sc_Jump : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce = 6.0f;
    [SerializeField] private int _jumpMax = 1;
    private int _jumpCurrent = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _jumpCurrent = _jumpMax;
    }

    public void TryJump(InputAction.CallbackContext context)
    {
        if (context.performed && _jumpCurrent > 0)
        {
            _jumpCurrent--;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void ResetJump()
    {
        _jumpCurrent = _jumpMax;
    }
}
