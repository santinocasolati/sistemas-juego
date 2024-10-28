using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 xLimits;
    [SerializeField] private Transform cameraLook;
    [SerializeField] private float movementSpeed;

    private RotationController rotationController;
    private MovementController movementController;

    private CharacterController characterController;

    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction movementAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions.FindAction("Look");
        movementAction = playerInput.actions.FindAction("Movement");

        characterController = GetComponent<CharacterController>();

        rotationController = new RotationController(rotationSpeed, transform, cameraLook, xLimits);
        movementController = new MovementController(characterController, movementSpeed);
    }

    private void Update()
    {
        if (rotationController != null)
        {
            Vector2 lookInput = lookAction.ReadValue<Vector2>();
            rotationController.UpdateCamera(lookInput, Time.deltaTime);
        }

        if (movementController != null)
        {
            Vector2 movementInput = movementAction.ReadValue<Vector2>();
            movementController.UpdateMovement(movementInput, transform, Time.deltaTime);
        }
    }
}
