using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Sc_Weapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponMuzzle;
    [SerializeField] private ParticleSystem _muzzleFlashVFX;
    [SerializeField][Min(-1)] private int _weaponDamage;
    [SerializeField][Min(-1)] private float _bulletImpactStrength;
    [SerializeField] private int _magSize;
    [SerializeField][Min(0.0f)] private float _reloadTime;
    [SerializeField][Min(0.0f)] private float _shootCooldown;
    [SerializeField][Min(0.0f)] private float _range;
    [SerializeField] private LayerMask _layerCheck;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _reloadSound;

    private int _magCurrentAmmo;
    private float _activeCooldown;
    private bool _reloading;
    private Damager _damager;
    private Animator _anim;

    public UnityEvent<int, int> WeaponShot;
    public UnityEvent<int, int> WeaponReloaded;

    public void Shoot()
    {
        if (_activeCooldown <= 0.0f)
        {
            if (_magCurrentAmmo > 0)
            {
                _magCurrentAmmo--;
                _muzzleFlashVFX.Play();
                WeaponShot.Invoke(_magCurrentAmmo, _magSize);
                AudioSource source = gameObject.GetComponent<AudioSource>();
                source.clip = _shotSound;
                source.Play();
                TraceBulletPath();
                _activeCooldown = _shootCooldown;
                _anim.Play("Gun_Fire", 0);
            }
            else
            {
                ReloadStart();
            }
        }
    }

    public void ReloadStart()
    {
        if (_activeCooldown <= 0.0f && _magCurrentAmmo < _magSize)
        {
            _activeCooldown = _reloadTime;
            _reloading = true;
            AudioSource source = gameObject.GetComponent<AudioSource>();
            source.clip = _reloadSound;
            source.Play();
            _anim.Play("Gun_Reload", 0);
        }
    }
    private void ReloadComplete()
    {
        _reloading = false;
        _magCurrentAmmo = _magSize;
        WeaponReloaded.Invoke(_magCurrentAmmo, _magSize);
    }

    private void TraceBulletPath()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, _range, _layerCheck);
        ShootParticleSystem(hit, hasHit);
        if (hasHit)
        {
            TryDamageBodyPart(hit);
        }
    }

    private void TryDamageBodyPart(RaycastHit hit)
    {

        Sc_Shootable shootable = hit.collider.GetComponentInParent<Sc_Shootable>();
        if (shootable)
        {
            shootable.ShootedAt(transform.position, _bulletImpactStrength);
        }

        Sc_CollisionPart bodyHit = hit.collider.GetComponent<Sc_CollisionPart>();
        Damageable damageable = hit.collider.GetComponentInParent<Damageable>();
        if (bodyHit && damageable)
        {
            switch (bodyHit.GetBodyPart)
            {
                case Sc_CollisionPart.BodyPart.NONE:

                    break;
                case Sc_CollisionPart.BodyPart.BODY:
                    _damager.DoDamage(damageable, _weaponDamage);
                    Debug.Log("Hit Body! Damage: " + _weaponDamage);
                    break;
                case Sc_CollisionPart.BodyPart.HEAD:
                    _damager.DoDamage(damageable, (float)_weaponDamage * 1.5f);
                    Debug.Log("Hit Head! Damage: " + (float)_weaponDamage * 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    private void Start()
    {
        _damager = GetComponentInParent<Damager>();
        _anim = GetComponent<Animator>();
        ReloadComplete();
    }
    private void Update()
    {
        //Debug.Log("Reloading? " + _reloading);
        //Debug.Log("Mag: " + _magCurrentAmmo + " / " + _magSize);
        if (_activeCooldown > 0.0f)
        {
            _activeCooldown -= Time.deltaTime;
            if (_activeCooldown <= 0.0f && _reloading == true)
            {
                ReloadComplete();
            }
        }
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _range, Color.red);
    }


    // From LlamAcademy, modified
    [SerializeField]
    private bool _addSpread = true;
    [SerializeField]
    private Vector3 _spreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem _impactParticleSystem;
    [SerializeField]
    private TrailRenderer _bulletTrail;
    [SerializeField]
    private float _particleSpeed = 100;



    public void ShootParticleSystem(RaycastHit rayHit, bool hasHit)
    {
        // Use an object pool instead for these! To keep this tutorial focused, we'll skip implementing one.
        // For more details you can see: https://youtu.be/fsDE_mO4RZM or if using Unity 2021+: https://youtu.be/zyzqA_CPz2E

        Vector3 direction = GetParticleSystemDirection();

        if (hasHit)
        {
            TrailRenderer trail = Instantiate(_bulletTrail, _weaponMuzzle.position, Quaternion.identity);

            StartCoroutine(MoveTrail(trail, rayHit.point, rayHit.normal, true));
        }
        // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything
        else
        {
            TrailRenderer trail = Instantiate(_bulletTrail, _weaponMuzzle.position, Quaternion.identity);

            StartCoroutine(MoveTrail(trail, _weaponMuzzle.position + GetParticleSystemDirection() * 100, Vector3.zero, false));
        }
    }

    private Vector3 GetParticleSystemDirection()
    {
        Vector3 direction = transform.forward;

        if (_addSpread)
        {
            direction += new Vector3(
                Random.Range(-_spreadVariance.x, _spreadVariance.x),
                Random.Range(-_spreadVariance.y, _spreadVariance.y),
                Random.Range(-_spreadVariance.z, _spreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator MoveTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, bool madeImpact)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= _particleSpeed * Time.deltaTime;

            yield return null;
        }
        trail.transform.position = hitPoint;
        if (madeImpact)
        {
            Instantiate(_impactParticleSystem, hitPoint, Quaternion.LookRotation(hitNormal));
        }

        Destroy(trail.gameObject, trail.time);
    }
}
