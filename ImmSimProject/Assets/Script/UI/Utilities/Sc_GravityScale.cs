using UnityEngine;

public class Sc_GravityScale : MonoBehaviour
{
    [SerializeField] private float gravityScale = 1.0f;

    public static float globalGravity = -9.81f;

    private Rigidbody _rigidbody;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
