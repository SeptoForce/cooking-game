using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    
    private bool _isWalking = false;
    public bool IsWalking() => _isWalking;
    private Vector3 _lastInteractDirection = Vector3.zero;
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
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
            if (hit.collider.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
        
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
}
