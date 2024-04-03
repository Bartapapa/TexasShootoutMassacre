using UnityEngine;

public class Sc_ProjectileDrag : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _drag;
    [SerializeField] AnimationCurve _curve;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 tmpVel = _rigidbody.velocity;
        tmpVel.y = 0.0f;
        Vector3 tmpDeltaVel = tmpVel.normalized;
        tmpDeltaVel = tmpDeltaVel * _drag;
        tmpVel -= tmpDeltaVel;
        tmpVel.y = _rigidbody.velocity.y;
        _rigidbody.velocity = tmpVel;
    }
}
