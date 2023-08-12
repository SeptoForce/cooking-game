using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public Action onInteract;
    public Action onInteractAlternate;
    
    private PlayerInputActions _playerInputActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Interact.performed += ctx =>
        {
            onInteract?.Invoke();
        };
        
        _playerInputActions.Player.InteractAlternate.performed += ctx =>
        {
            onInteractAlternate?.Invoke();
        };
    }

    public Vector2 GetMovementVector()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}
