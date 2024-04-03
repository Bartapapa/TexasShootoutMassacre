//By ALBERT Esteban
using UnityEngine;

public class LevelReferences : Singleton<LevelReferences>
{
    [SerializeField] Camera _uiCamera;
    [SerializeField] GameObject _player = null;
    [SerializeField] GameObject _station = null;
    [SerializeField] AudioSource _musicPlayer = null;

    public Camera UICamera => _uiCamera;
    public GameObject Player => _player;
    public GameObject Station => _station;
    public AudioSource MusicPlayer => _musicPlayer;
}
