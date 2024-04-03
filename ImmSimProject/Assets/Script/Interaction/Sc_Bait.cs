using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Bait : MonoBehaviour
{
    [SerializeField] private float _baitLifeTime = 10.0f;
    [SerializeField] private float _baitDuration = 1.0f;
    //[SerializeField] private bool _isPoisoned = false;
    private bool _isBeingConsumed = false;
    private float _lifeTime;
    private float _duration;
    private Sc_AIStateManager _stateManager;


    private void Start()
    {
        _lifeTime = _baitLifeTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CheckCollidingAI(collision, out Sc_AIStateManager stateManager))
        {
            _stateManager = stateManager;
            _isBeingConsumed = true;
            _lifeTime = _baitLifeTime;
            _duration = _baitDuration;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (CheckCollidingAI(collision, out Sc_AIStateManager stateManager))
        {
            _isBeingConsumed = true;
            _lifeTime = _baitLifeTime;
        }
    }
    private void Update()
    {
        if (!_isBeingConsumed)
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime < 0.0f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            _duration -= Time.deltaTime;
            if (_duration < 0.0f)
            {
                if (_stateManager)
                {
                    _stateManager.ChangeAIState(Sc_AIStateManager.AIState.PATROLLING);
                }
                Destroy(gameObject);
            }
        }
    }
    private bool CheckCollidingAI(Collision collision, out Sc_AIStateManager stateManager)
    {
        stateManager = collision.gameObject.GetComponentInParent<Sc_AIStateManager>();
        if (stateManager)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
