using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Crouch : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    private Sc_CharacterMovement _characterMovement;
    private Vector3 _startingLocalPosition;
    private float _startingHeight;

    private void Awake()
    {
        _characterMovement = GetComponentInParent<Sc_CharacterMovement>();
        _startingLocalPosition = _cameraPivot.localPosition;
        _startingHeight = _capsuleCollider.height;
    }

    private void Update()
    {
        SetCameraAndCollider(_characterMovement.GetMovementState == Sc_CharacterMovement.MovementState.CROUCHING);
    }

    private void SetCameraAndCollider(bool isCrouching)
    {
        if (isCrouching)
        {
            _cameraPivot.localPosition = _startingLocalPosition + Vector3.down * 0.5f;
            _capsuleCollider.height = _startingHeight * 0.5f;
        }
        else
        {
            _cameraPivot.localPosition = _startingLocalPosition;
            _capsuleCollider.height = _startingHeight;
        }
    }
}
