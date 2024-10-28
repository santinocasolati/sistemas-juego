using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController
{
    private float _rotationSpeed;
    private Transform _transform;
    private Transform _cameraTransform;
    private Vector2 _xLimits;
    private float _pitch;
    private float _yaw;

    public RotationController(float rotationSpeed, Transform transform, Transform cameraTransform, Vector2 xLimits)
    {
        _rotationSpeed = rotationSpeed;
        _transform = transform;
        _xLimits = xLimits;
        _cameraTransform = cameraTransform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _pitch = _cameraTransform.eulerAngles.x;
        _yaw = _transform.eulerAngles.y;
    }

    public void UpdateCamera(Vector2 input, float deltaTime)
    {
        _pitch -= input.y * _rotationSpeed * deltaTime;
        _yaw += input.x * _rotationSpeed * deltaTime;

        _pitch = Mathf.Clamp(_pitch, _xLimits.x, _xLimits.y);

        _transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
        _cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
