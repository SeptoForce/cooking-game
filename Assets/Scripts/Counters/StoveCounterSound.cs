using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        stoveCounter.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(StoveCounter.State obj)
    {
        bool playSound = obj == StoveCounter.State.Frying || obj == StoveCounter.State.Fried;
        if (playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
    }
}
