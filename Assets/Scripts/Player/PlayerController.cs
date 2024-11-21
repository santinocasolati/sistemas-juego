using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 xLimits;
    [SerializeField] private Transform cameraLook;
    [SerializeField] private float movementSpeed;
    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCamera aimingCamera;

    private RotationController rotationController;
    private MovementController movementController;
    private CameraController cameraController;

    private Rigidbody rb;

    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction movementAction;
    private InputAction aimAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions.FindAction("Look");
        movementAction = playerInput.actions.FindAction("Movement");
        aimAction = playerInput.actions.FindAction("Aim");

        rb = GetComponent<Rigidbody>();

        rotationController = new RotationController(rotationSpeed, transform, cameraLook, xLimits);
        movementController = new MovementController(rb, movementSpeed);
        cameraController = new CameraController(thirdPersonCamera, aimingCamera);
    }

    private void OnEnable()
    {
        lookAction.performed += ctx => rotationController?.UpdateCamera(ctx.ReadValue<Vector2>(), Time.deltaTime);
        
        aimAction.performed += _ => cameraController.SetAim(true);
        aimAction.canceled += _ => cameraController.SetAim(false);
    }

    private void OnDisable()
    {
        lookAction.performed -= ctx => rotationController?.UpdateCamera(ctx.ReadValue<Vector2>(), Time.deltaTime);

        aimAction.performed -= _ => cameraController.SetAim(true);
        aimAction.canceled -= _ => cameraController.SetAim(false);
    }

    private void Update()
    {
        movementController?.UpdateMovement(movementAction.ReadValue<Vector2>(), transform);
    }
}
