using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    
    public event Action OnStateChange;
    
    public static KitchenGameManager Instance { get; private set; }

    private State _state;
    private float _waitToStartTimer = 1f;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer = 60f;

    private void Awake()
    {
        Instance = this;
        _state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitToStartTimer -= Time.deltaTime;
                if (_waitToStartTimer <= 0)
                {
                    _state = State.CountdownToStart;
                    OnStateChange?.Invoke();
                }
                break;
            case State.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer <= 0)
                {
                    _state = State.GamePlaying;
                    OnStateChange?.Invoke();
                }
                break;
            case State.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer <= 0)
                {
                    _state = State.GameOver;
                    OnStateChange?.Invoke();
                }
                break;
            case State.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }
    
    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }
    
    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }
}
