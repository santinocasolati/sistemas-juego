using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    private Rigidbody _rb;
    private float _movementSpeed;

    public MovementController(Rigidbody rb, float movementSpeed)
    {
        _rb = rb;
        _movementSpeed = movementSpeed;
    }

    public void UpdateMovement(Vector2 input, Transform transform)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 horizontalMovement = (forward * input.y + right * input.x) * _movementSpeed;

        Vector3 velocity = _rb.velocity;
        velocity.x = horizontalMovement.x;
        velocity.z = horizontalMovement.z;
        _rb.velocity = velocity;
    }
}
