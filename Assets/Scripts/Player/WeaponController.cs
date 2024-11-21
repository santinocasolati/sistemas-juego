using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController
{
    private Transform _weaponSlot;
    private GameObject _currentWeapon;
    private IWeapon _currentWeaponController;
    private Transform _aimTarget;

    public WeaponController(Transform weaponSlot, GameObject initialWeaponPrefab, Transform aimTarget)
    {
        _weaponSlot = weaponSlot;
        _aimTarget = aimTarget;

        ChangeWeapon(initialWeaponPrefab);
    }

    public void ChangeWeapon(GameObject weaponPrefab)
    {
        if (_currentWeapon != null)
            GameObject.Destroy(_currentWeapon);

        _currentWeapon = GameObject.Instantiate(weaponPrefab, _weaponSlot);
        _currentWeaponController = _currentWeapon.GetComponent<IWeapon>();
        _currentWeaponController.Setup(_aimTarget);
    }

    public void ShootState(bool isShooting)
    {
        if (_currentWeaponController != null)
            _currentWeaponController.CanShoot = isShooting;
    }
}
