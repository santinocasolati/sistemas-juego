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
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private GameObject console;

    private RotationController rotationController;
    private MovementController movementController;
    private CameraController cameraController;
    private WeaponController weaponController;

    private Rigidbody rb;

    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction movementAction;
    private InputAction aimAction;
    private InputAction shootAction;
    private InputAction weaponOneAction;
    private InputAction weaponTwoAction;
    private InputAction toggleConsoleAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions.FindAction("Look");
        movementAction = playerInput.actions.FindAction("Movement");
        aimAction = playerInput.actions.FindAction("Aim");
        shootAction = playerInput.actions.FindAction("Shoot");
        weaponOneAction = playerInput.actions.FindAction("WeaponOne");
        weaponTwoAction = playerInput.actions.FindAction("WeaponTwo");
        toggleConsoleAction = playerInput.actions.FindAction("ToggleConsole");

        rb = GetComponent<Rigidbody>();

        rotationController = new RotationController(rotationSpeed, transform, cameraLook, xLimits);
        movementController = new MovementController(rb, movementSpeed, GetComponentInChildren<Animator>());
        cameraController = new CameraController(thirdPersonCamera, aimingCamera);
        weaponController = new WeaponController(weaponSlot, weapons[0]);
    }

    private void OnEnable()
    {
        lookAction.performed += ctx => rotationController?.UpdateCamera(ctx.ReadValue<Vector2>(), Time.deltaTime);
        
        aimAction.performed += _ => cameraController.SetAim(true);
        aimAction.canceled += _ => cameraController.SetAim(false);

        shootAction.performed += _ => weaponController.ShootState(true);
        shootAction.canceled += _ => weaponController.ShootState(false);

        weaponOneAction.performed += _ => weaponController.ChangeWeapon(weapons[0]);
        weaponTwoAction.performed += _ => weaponController.ChangeWeapon(weapons[1]);

        toggleConsoleAction.performed += _ => console.SetActive(!console.activeSelf);
    }

    private void OnDisable()
    {
        lookAction.performed -= ctx => rotationController?.UpdateCamera(ctx.ReadValue<Vector2>(), Time.deltaTime);

        aimAction.performed -= _ => cameraController.SetAim(true);
        aimAction.canceled -= _ => cameraController.SetAim(false);

        shootAction.performed -= _ => weaponController.ShootState(true);
        shootAction.canceled -= _ => weaponController.ShootState(false);

        weaponOneAction.performed -= _ => weaponController.ChangeWeapon(weapons[0]);
        weaponTwoAction.performed -= _ => weaponController.ChangeWeapon(weapons[1]);

        toggleConsoleAction.performed -= _ => console.SetActive(!console.activeSelf);
    }

    private void Update()
    {
        movementController?.UpdateMovement(movementAction.ReadValue<Vector2>(), transform);

        CalculateAimTarget();
    }

    private void CalculateAimTarget()
    {
        Vector3 pos;

        if (cameraController.isAiming)
        {
            pos = new Vector3(1.2f, .5f, 0);
        } else
        {
            pos = new Vector3(.8f, .5f, 0);
        }

        Ray cameraRay = Camera.main.ViewportPointToRay(pos);
        aimTarget.position = cameraRay.GetPoint(10);
    }
}
