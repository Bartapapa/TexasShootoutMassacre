using UnityEngine;

public class Sc_Shootable : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool _canBeDestroyed = false;
    [SerializeField] private float _hitForceThreshold = -1f;

    [Header("Object refs")]
    [SerializeField] private ParticleSystem glassBreak;

    private Rigidbody _rigidbody;

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
        //_rigidbody.velocity += shootDir * hitForce;

        if (hitForce >= _hitForceThreshold && _canBeDestroyed)
        {
            DestroyShootable();
        }
        else
        {
            _rigidbody.AddForce(shootDir * hitForce, ForceMode.Impulse);
        }
    }

    public void DestroyShootable()
    {
        if (glassBreak)
        {
            ParticleSystem newParticle = Instantiate<ParticleSystem>(glassBreak, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
