using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player _player;
    private float _footstepTimer;
    private float _footstepTimerMax = 0.25f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player.IsWalking())
        {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer <= 0)
            {
                _footstepTimer = _footstepTimerMax;
                SoundManager.Instance.PlayFootstepSound(transform.position);
            }
        }
        else
        {
            _footstepTimer = 0;
        }
    }
}
