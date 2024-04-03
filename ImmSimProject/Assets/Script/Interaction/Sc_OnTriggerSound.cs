using UnityEngine;

public class Sc_OnTriggerSound : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private bool _triggerOnce = false;
    private bool _hasBeenTriggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if (_triggerOnce && _hasBeenTriggered == true)
        {
            return;
        }
        PlaySources();
        _hasBeenTriggered = true;
    }


    public void PlaySources()
    {
        foreach (AudioSource source in _audioSources)
        {
            source.Play();
        }
    }
}
