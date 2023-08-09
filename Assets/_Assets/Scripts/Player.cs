using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    
    private bool _isWalking = false;
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y);
        
        _isWalking = movementVector != Vector3.zero;

        transform.position += movementVector * (Time.deltaTime * movementSpeed);
        
        transform.forward = Vector3.Slerp(transform.forward, movementVector, Time.deltaTime * 10f);
    }
    
    public bool IsWalking()
    {
        return _isWalking;
    }
}
