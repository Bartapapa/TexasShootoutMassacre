using UnityEngine;

public class Sc_DeathSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _audioSource;

    public void PlayDeathSound()
    {
        //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = _deathSound;
        //Debug.Log(audioSource.clip);
        //audioSource.Play();
        AudioSource source = Instantiate<AudioSource>(_audioSource, gameObject.transform.position, Quaternion.identity);
        source.clip = _deathSound;
        source.Play();
        Destroy(source.gameObject, 3.0f);

    }
}
