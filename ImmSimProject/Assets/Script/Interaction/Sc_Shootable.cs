using UnityEngine;

public class Sc_Shootable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _weight;

    private void Start()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }
    public void ShootedAt(Vector3 hitOrigin, float hitForce)
    {
        Vector3 shootDir = (transform.position - hitOrigin).normalized;
        //Vector3 shootDelta = -shootDir;
        
        //shootDir = shootDir * hitForce;

        //shootDelta = shootDelta * ();
        _rigidbody.velocity += shootDir * hitForce;
    }
}
