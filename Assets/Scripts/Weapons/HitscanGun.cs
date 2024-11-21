using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _gunTip;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;

    private Transform _aimTarget;

    private bool _canShoot = false;
    private bool _isShooting = false;
    public bool CanShoot
    {
        set {
            _canShoot = value;
            _isShooting = value;
        }
    }

    public void Setup(Transform aimTarget)
    {
        transform.localPosition = _offset;
        transform.localRotation = Quaternion.Euler(_rotation);
        _aimTarget = aimTarget;
    }

    public void Shoot()
    {
        if (!_canShoot) return;
        _canShoot = false;

        Vector3 direction = _aimTarget.position - _gunTip.position;
        direction.Normalize();

        if (Physics.Raycast(_gunTip.position, direction, out RaycastHit hit, _maxDistance))
        {
            SpawnHitEffect(hit.point, hit.normal);
        }

        Invoke(nameof(ResetShoot), _shootDelay);
    }

    private void Update()
    {
        if (_isShooting)
            Shoot();
    }

    void SpawnHitEffect(Vector3 position, Vector3 normal)
    {
        Debug.DrawLine(_gunTip.position, position, Color.red, 1f);
    }

    private void ResetShoot()
    {
        if (_isShooting)
            _canShoot = true;
    }
}
