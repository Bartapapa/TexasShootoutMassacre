using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sc_DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    public delegate void DefaultEvent();
    public DefaultEvent OnDialogueBoxStarted;
    public DefaultEvent OnDialogueBoxEnded;

    private float _lifeTimeTimer = -1f;
    private bool _dialogueBoxReachedDuration = false;

    public void GenerateDialogueBox(DialogueLine lines)
    {
        PopulateLines(lines.Lines);
        SetLifetimeTimer(lines.Duration);

        OnDialogueBoxStarted?.Invoke();
    }

    private void PopulateLines(string lines)
    {
        textBox.text = lines;
    }

    private void SetLifetimeTimer(float duration)
    {
        _lifeTimeTimer = duration;
    }

    private void Update()
    {
        if (_lifeTimeTimer > 0)
        {
            _lifeTimeTimer -= Time.deltaTime;
        }
        else
        {
            if (!_dialogueBoxReachedDuration)
            {
                _dialogueBoxReachedDuration = true;
                OnDialogueBoxEnded?.Invoke();
            }
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
