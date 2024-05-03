using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public GameObject Speaker;
    [TextArea(5, 20)]
    public string Lines;
    public float Duration = 1f;
    public bool Shout = false;
}

public class Sc_Dialoguer : MonoBehaviour
{
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;

    public Sc_DialogueBox DialogueBoxPrefab;
    public List<DialogueLine> DialogueLines = new List<DialogueLine>();

    private int _currentLineIndex = -1;
    private Sc_DialogueBox _currentDialogueBox;

    public void StartDialogue()
    {
        OnDialogueStart.Invoke();

        GoToNextLine();
    }

    public void EndDialogue()
    {
        if (_currentDialogueBox)
        {
            RemoveCurrentDialogueBox();
        }

        OnDialogueEnd.Invoke();
        _currentLineIndex = -1;
    }

    public void GoToNextLine()
    {
        if (CheckNextLine())
        {
            _currentLineIndex++;
            if (_currentDialogueBox)
            {
                RemoveCurrentDialogueBox();
            }
            if (DialogueLines[_currentLineIndex].Speaker == null)
            {
                Debug.LogWarning("No speaker for this line. Ending dialogue.");
                EndDialogue();
            }
            else
            {
                _currentDialogueBox = Instantiate<Sc_DialogueBox>(DialogueBoxPrefab,
                    DialogueLines[_currentLineIndex].Speaker.transform.position + (Vector3.up * 2.5f),
                    Quaternion.identity);
                if (DialogueLines[_currentLineIndex].Shout)
                {
                    _currentDialogueBox.GenerateDialogueBox(DialogueLines[_currentLineIndex], 40);
                }
                else
                {
                    _currentDialogueBox.GenerateDialogueBox(DialogueLines[_currentLineIndex]);
                }
                
                _currentDialogueBox.OnDialogueBoxEnded += OnCurrentDialogueBoxEnded;
            }       
        }
        else
        {
            EndDialogue();
        }
    }

    private bool CheckNextLine()
    {
        if (_currentLineIndex + 1 < DialogueLines.Count)
        {
            //There is a next line
            return true;
        }
        else
        {
            //nope
            return false;
        }
    }

    private void RemoveCurrentDialogueBox()
    {
        _currentDialogueBox.OnDialogueBoxEnded -= OnCurrentDialogueBoxEnded;
        Destroy(_currentDialogueBox.gameObject);
        _currentDialogueBox = null;
    }

    private void OnCurrentDialogueBoxEnded()
    {
        GoToNextLine();
    }
}
