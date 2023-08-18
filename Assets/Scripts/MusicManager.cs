using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    
    private AudioSource _audioSource;
    private float _volume = 0.9f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _volume = PlayerPrefs.GetFloat("musicVolume", _volume);
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void ScrollVolume()
    {
        _volume += .1f;
        if (_volume > 1f)
        {
            _volume = 0f;
        }
        
        PlayerPrefs.SetFloat("musicVolume", _volume);
        _audioSource.volume = _volume;
    }
    
    public float GetVolume()
    {
        return _volume;
    }
}
