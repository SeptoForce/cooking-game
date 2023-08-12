using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    
    public static Player Instance { get; private set; }
    
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
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameInput.onInteract += InteractAction;
        gameInput.onInteractAlternate += InteractAlternateAction;
    }

    private void Update()
    {
        HandleMovement();
        HandleSelection();
    }

    private void InteractAction()
    {
        _selectedCounter?.Interact(this);
    }
    
    private void InteractAlternateAction()
    {
        _selectedCounter?.InteractAlternate(this);
    }

    private void HandleSelection()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
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
        Vector2 inputVector = gameInput.GetMovementVector();
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
