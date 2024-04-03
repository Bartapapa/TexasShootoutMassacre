using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sc_UI_ThrowUnlockCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Sc_ThrowBait _throw;

    private void Update()
    {
        if (_text.enabled)
        {
            return;
        }
        if (_throw.throwUnlocked)
        {
            _text.enabled = true;
        }
    }
}
