using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class Sc_CharacterMovement : MonoBehaviour
{
    public enum MovementState
    {
        NONE,
        WALKING,
        RUNNING,
        CROUCHING
    }

    private Rigidbody _rigidbody;
    [SerializeField][Min(0.01f)] private float _walkSpeed = 6f;
    [SerializeField][Min(0.01f)] private float _runSpeed = 6f;
    [SerializeField][Min(0.01f)] private float _crouchSpeed = 6f;
    [SerializeField][Min(0.01f)] private float _gravityScale = 1f;

    [SerializeField] private Transform _cameraPivot;


    private Vector2 _movementInput = Vector2.zero;
    private Vector3 _relativeMovementInput = Vector3.zero;
    private Vector3 _velocityVector;
    private MovementState _movementState = MovementState.WALKING;

    public MovementState GetMovementState => _movementState;


    public void MoveInput(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    public void RunInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movementState = MovementState.RUNNING;
        }
        if (context.canceled)
        {
            _movementState = MovementState.WALKING;
        }
    }
    public void CrouchInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movementState = MovementState.CROUCHING;
        }
        if (context.canceled)
        {
            _movementState = MovementState.WALKING;
        }
    }

    private void ApplyMovement(float movementSpeed)
    {
        _velocityVector = _rigidbody.velocity;
        _velocityVector.x = movementSpeed * _relativeMovementInput.x;
        _velocityVector.z = movementSpeed * _relativeMovementInput.z; //Using z due to Vector2 > Vector3 conversion in Update
        _rigidbody.velocity = _velocityVector;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 camForward = _cameraPivot.forward;
        camForward.y = 0.0f;

        Vector3 camRight = _cameraPivot.right;
        camRight.y = 0.0f;

        _relativeMovementInput = (camForward*_movementInput.y) + (camRight*_movementInput.x);
        _relativeMovementInput = _relativeMovementInput.normalized;
    }
    private void FixedUpdate()
    {
        switch (_movementState)
        {
            case MovementState.NONE:
                break;
            case MovementState.WALKING:
                ApplyMovement(_walkSpeed);
                break;
            case MovementState.RUNNING:
                ApplyMovement(_runSpeed);
                break;
            case MovementState.CROUCHING:
                ApplyMovement(_crouchSpeed);
                break;
            default:
                break;
        }
        //_rigidbody.velocity += Physics.gravity * _gravityScale;
        Vector3 gravity = -9.81f * _gravityScale * Vector3.up;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);

    }

}
