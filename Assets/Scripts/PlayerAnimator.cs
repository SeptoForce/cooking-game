using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour
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
        if(!IsOwner) return;
        
        _animator.SetBool(_isWalking, player.IsWalking());
    }
}
