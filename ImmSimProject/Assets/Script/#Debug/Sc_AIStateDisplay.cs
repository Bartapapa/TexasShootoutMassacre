using UnityEngine;
using TMPro;

public class Sc_AIStateDisplay : MonoBehaviour
{
    private Sc_AIStateManager _stateManager;
    private TextMeshPro _textMesh;
    private void Awake()
    {
        _stateManager = GetComponentInParent<Sc_AIStateManager>();
        _textMesh = GetComponent<TextMeshPro>();
        if (_stateManager == null || _textMesh == null)
        {
            Debug.LogWarning("AIStateDisplay: State Manager or Text Mesh not found, display will not update!");
            enabled = false;
        }
    }

    private void OnEnable()
    {
        _stateManager.AIStateChanged.RemoveListener(UpdateStateDisplay);
        _stateManager.AIStateChanged.AddListener(UpdateStateDisplay);
    }
    private void OnDisable()
    {
        _stateManager.AIStateChanged.RemoveListener(UpdateStateDisplay);
    }

    private void UpdateStateDisplay(Sc_AIStateManager.AIState displayState)
    {
        _textMesh.text = displayState.ToString();
    }
}
