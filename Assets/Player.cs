using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    private void Update()
    {
        Vector2 inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        
        inputVector.Normalize();
        
        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += movementVector * (Time.deltaTime * movementSpeed);
        
        transform.forward = Vector3.Slerp(transform.forward, movementVector, Time.deltaTime * 10f);
    }
}
