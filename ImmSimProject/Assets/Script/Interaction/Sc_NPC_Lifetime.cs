using UnityEngine;
using TMPro;

public class Sc_NPC_Lifetime : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private float _lifeDuration;
    private bool _textDeadUpdated = false;

    private void OnTriggerEnter(Collider other)
    {
        Sc_CameraController cameraController = other.gameObject.GetComponentInParent<Sc_CameraController>();
        if (cameraController)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (_textDeadUpdated)
        {
            return;
        }
        _lifeDuration -= Time.deltaTime;
        if (_lifeDuration < 0.0f)
        {
            _text.text = "Dead, report to town (end quest B)";
        }
    }
}
