using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private LayerMask counterLayerMask;
    
    public static event Action OnAnyPlayerSpawned;
    public event Action<Player> OnPickedUpSomething;
    public static event Action<Player> OnAnyPickedUpSomething;
    public static Player LocalInstance { get; private set; }
    
    //For the interface IKitchenObjectParent
    private KitchenObject _kitchenObject;
    [SerializeField] private GameObject kitchenObjectHoldPoint;

    private bool _isWalking = false;
    public bool IsWalking() => _isWalking;
    private Vector3 _lastInteractDirection;
    
    private BaseCounter _selectedCounter;
    
    public EventHandler<SelectedCounterChangedEventArgs> onSelectedCounterChanged;

    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    private void Awake()
    {
        // Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            LocalInstance = this;
        }
        
        OnAnyPlayerSpawned?.Invoke();
    }

    private void Start()
    {
        GameInput.Instance.OnInteract += InteractAction;
        GameInput.Instance.OnInteractAlternate += InteractAlternateAction;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnInteract -= InteractAction;
        GameInput.Instance.OnInteractAlternate -= InteractAlternateAction;
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        HandleMovement();
        HandleSelection();
    }

    private void InteractAction()
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }

    }
    
    private void InteractAlternateAction()
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }

    }

    private void HandleSelection()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (movementDirection != Vector3.zero)
        {
            _lastInteractDirection = movementDirection;
        }
        
        float interactDistance = 1f;
        
        Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit hit, interactDistance, counterLayerMask);
        
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out BaseCounter baseCounter))
            {
                SetSelectedCounter(baseCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        _selectedCounter = baseCounter;
        onSelectedCounterChanged?.Invoke(this, new SelectedCounterChangedEventArgs
        {
            SelectedCounter = _selectedCounter
        });
    }


    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);
        _isWalking = movementDirection != Vector3.zero;
        
        float moveDistance = movementSpeed * Time.deltaTime;
        float playerRadius = 0.6f;
        float playerHeight = 2f;
        
        bool canMove = !Physics.CapsuleCast(transform.position + Vector3.up * playerHeight,
            transform.position + Vector3.up * playerRadius/2 ,
            playerRadius, movementDirection, moveDistance);
        if (canMove)
        {
            transform.position += movementDirection * moveDistance;
        }
        else
        {
            Vector3[] testDirections = new Vector3[]
            {
                new Vector3(movementDirection.x, 0, 0),
                new Vector3(0, 0, movementDirection.z)
            };
            foreach (Vector3 testDirection in testDirections)
            {
                canMove = !Physics.CapsuleCast(transform.position + Vector3.up * playerHeight,
                    transform.position+ Vector3.up * playerRadius/2,
                    playerRadius, testDirection, moveDistance);
                if (canMove)
                {
                    testDirection.Normalize();
                    transform.position += testDirection * moveDistance;
                    break;
                }
            }
        }
        
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * 10f);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint.transform;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
        OnPickedUpSomething?.Invoke(this);
        OnAnyPickedUpSomething?.Invoke(this);
    }
    
    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
    
    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
