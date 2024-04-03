using UnityEngine;
using UnityEngine.Events;

public class Sc_GroundDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _checkSize;
    [SerializeField] private float _checkDistance;
    [SerializeField] private LayerMask _checkedLayer;
    private bool _airborneFlag = false;
    public UnityEvent GroundHit;
    public bool GroundCheck()
    {
        return Physics.BoxCast(
            transform.position+transform.up, //Won't detect floor if cast starts in it, offset to start in player
            _checkSize,
            -transform.up,
            transform.rotation,
            _checkDistance,
            _checkedLayer);
    }

    private void Update()
    {
        if (GroundCheck())
        {
            if (_airborneFlag)
            {
                GroundHit.Invoke();
                _airborneFlag = false;
            }
        }
        else
        {
            _airborneFlag = true;
        }
    }
    private void OnDrawGizmos()
    {
        ExtDebug.DrawBoxCastBox(
            transform.position+transform.up,
            _checkSize,
            transform.rotation,
            -transform.up,
            _checkDistance,
            Color.red);
    }
}
