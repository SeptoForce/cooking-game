using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    private Animator _animator;
    private readonly int _isWalking = Animator.StringToHash("isWalking");
    
    [SerializeField] private Player player;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        _animator.SetBool(_isWalking, player.IsWalking());
    }
}
