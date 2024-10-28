using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    private CharacterController _characterController;
    private float _movementSpeed;

    public MovementController(CharacterController characterController, float movementSpeed)
    {
        _characterController = characterController;
        _movementSpeed = movementSpeed;
    }

    public void UpdateMovement(Vector2 input, Transform transform, float deltaTime)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 movement = (forward * input.y + right * input.x) * _movementSpeed * deltaTime;
        _characterController.Move(movement);
    }
}
