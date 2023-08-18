using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event Action OnInteract;
    public event Action OnInteractAlternate;
    public event Action OnPause;
    
    public static GameInput Instance { get; private set; }
    
    private PlayerInputActions _playerInputActions;
    private void Awake()
    {
        Instance = this;
        
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Interact.performed += OnInteractAction;
        _playerInputActions.Player.InteractAlternate.performed += OnInteractAlternateAction;
        _playerInputActions.Player.Pause.performed += OnPauseAction;
    }

    private void OnPauseAction(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke();
    }

    private void OnInteractAlternateAction(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke();
    }

    private void OnInteractAction(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= OnInteractAction;
        _playerInputActions.Player.InteractAlternate.performed -= OnInteractAlternateAction;
        _playerInputActions.Player.Pause.performed -= OnPauseAction;
        _playerInputActions.Dispose();
    }

    public Vector2 GetMovementVector()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}
