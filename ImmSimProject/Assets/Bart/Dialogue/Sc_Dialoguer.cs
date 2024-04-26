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
}

public class Sc_Dialoguer : MonoBehaviour
{
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;

    public List<DialogueLine> DialogueLines = new List<DialogueLine>();

    private int _currentLineIndex = -1;

    public void StartDialogue()
    {
        OnDialogueStart.Invoke();
    }

    public void EndDialogue()
    {
        OnDialogueEnd.Invoke();
        _currentLineIndex = -1;
    }

    public void GoToNextLine()
    {
        if (CheckNextLine())
        {
            _currentLineIndex++;
            //Current line = DialogueLines[currentLineIndex];
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
}
