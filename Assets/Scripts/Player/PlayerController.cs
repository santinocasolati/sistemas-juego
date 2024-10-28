using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 xLimits;
    [SerializeField] private Transform cameraLook;

    private RotationController rotationController;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rotationController = new RotationController(rotationSpeed, transform, cameraLook, xLimits);
    }

    private void Update()
    {
        Vector2 lookInput = playerInput.actions.FindAction("Look").ReadValue<Vector2>();

        if (rotationController != null)
            rotationController.UpdateCamera(lookInput, Time.deltaTime);
    }
}
