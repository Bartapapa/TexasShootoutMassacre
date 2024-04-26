using System;
using UnityEngine;
using UnityEngine.InputSystem;

#region structs
[Serializable]
public struct CameraSensitivity
{
    [Min(0.01f)] public float horizontal;
    [Min(0.01f)] public float vertical;

    public CameraSensitivity(float fhorizontal, float fvertical)
    {
        horizontal = fhorizontal;
        vertical = fvertical;
    }

}

[Serializable]
public struct CameraAngleLimit
{
    public float min;
    public float max;
}
public struct CameraRotation
{
    public float Yaw;
    public float Pitch;
}
#endregion

public class Sc_CameraController : MonoBehaviour
{
    public static Sc_CameraController Instance;

    [SerializeField] private Transform _cameraPivot;
    private Vector2 _lookInput = Vector2.zero;

    [SerializeField] private CameraSensitivity _cameraSensitivity;
    [SerializeField] private CameraAngleLimit _cameraAngleLimits;
    [SerializeField] private CameraSensitivity _aimingCameraSensitivity;
    [SerializeField] private float _aimingFOV;
    private CameraRotation _cameraRotation;

    private bool _playerControlled = true;
    public bool PlayerControlled
    {
        get
        {
            return _playerControlled;
        }
        set
        {
            _playerControlled = value;
        }
    }
    private bool _isAiming = false;
    private CameraSensitivity _originSensitivity;
    private float _originFOV;
    private CameraSensitivity _currentCameraSensitivity;

    private void Awake()
    {
        if (Instance == null)
        {
            //Debug.LogWarning("1" + this.gameObject.name);
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("2" + this.gameObject.name);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _originSensitivity = _cameraSensitivity;
        _currentCameraSensitivity = _originSensitivity;
        _originFOV = Camera.main.fieldOfView;
    }
    private void Update()
    {
        HandleFOV();
        HandleSensitivity();

        _cameraRotation.Yaw += _lookInput.x * _currentCameraSensitivity.horizontal * Time.deltaTime;
        _cameraRotation.Pitch -= _lookInput.y * _currentCameraSensitivity.vertical * Time.deltaTime; //Y input is inverse so we're substracting
        _cameraRotation.Pitch = Mathf.Clamp(_cameraRotation.Pitch, _cameraAngleLimits.min, _cameraAngleLimits.max);
    }

    public void ToggleAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isAiming = true;
            Debug.Log("???");
        }

        if (context.canceled)
        {
            _isAiming = false;
            Debug.Log("!!!");
        }

    }

    private void HandleSensitivity()
    {
        if (_isAiming)
        {
            _currentCameraSensitivity = new CameraSensitivity(
                Mathf.Lerp(_currentCameraSensitivity.horizontal, _aimingCameraSensitivity.horizontal, .1f),
                Mathf.Lerp(_currentCameraSensitivity.vertical, _aimingCameraSensitivity.vertical, .1f));
        }
        else
        {
            _currentCameraSensitivity = new CameraSensitivity(
                Mathf.Lerp(_currentCameraSensitivity.horizontal, _originSensitivity.horizontal, .1f),
                Mathf.Lerp(_currentCameraSensitivity.vertical, _originSensitivity.vertical, .1f));
        }
    }

    private void HandleFOV()
    {
        if (_isAiming)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _aimingFOV, .1f);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _originFOV, .1f);
        }
    }

    private void LateUpdate()
    {
        if (_playerControlled)
        {
            _cameraPivot.localEulerAngles = new Vector3(_cameraRotation.Pitch, _cameraRotation.Yaw, 0.0f);
        }
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }
}
