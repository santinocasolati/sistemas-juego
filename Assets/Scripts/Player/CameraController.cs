using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    private CinemachineVirtualCamera _thirdPersonCamera;
    private CinemachineVirtualCamera _aimingCamera;

    public bool isAiming = false;

    public CameraController(CinemachineVirtualCamera thirdPersonCamera, CinemachineVirtualCamera aimingCamera)
    {
        _thirdPersonCamera = thirdPersonCamera;
        _aimingCamera = aimingCamera;
    }

    public void SetAim(bool state)
    {
        _thirdPersonCamera.Priority = state ? 0 : 1;
        _aimingCamera.Priority = !state ? 0 : 1;

        isAiming = state;
    }
}
