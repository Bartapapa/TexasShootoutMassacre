using System;
using UnityEngine;
using UnityEngine.InputSystem;

#region structs
[Serializable]
public struct CameraSensitivity
{
    [Min(0.01f)] public float horizontal;
    [Min(0.01f)] public float vertical;
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
    [SerializeField] private Transform _cameraPivot;
    private Vector2 _lookInput = Vector2.zero;

    [SerializeField] private CameraSensitivity _cameraSensitivity;
    [SerializeField] private CameraAngleLimit _cameraAngleLimits;
    private CameraRotation _cameraRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        _cameraRotation.Yaw += _lookInput.x * _cameraSensitivity.horizontal * Time.deltaTime;
        _cameraRotation.Pitch -= _lookInput.y * _cameraSensitivity.vertical * Time.deltaTime; //Y input is inverse so we're substracting
        _cameraRotation.Pitch = Mathf.Clamp(_cameraRotation.Pitch, _cameraAngleLimits.min, _cameraAngleLimits.max);
    }
    private void LateUpdate()
    {
        _cameraPivot.localEulerAngles = new Vector3(_cameraRotation.Pitch, _cameraRotation.Yaw, 0.0f);
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }
}
