using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform gunTip;
    [SerializeField] private float maxDistance;
    [SerializeField] private float shootDelay;
    [SerializeField] private float damage;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private LayerMask layerMask;

    private bool _canShoot = true;
    private bool _isShooting = false;
    public bool IsShooting
    {
        set {
            _isShooting = value;
        }
    }

    public void Setup()
    {
        transform.localPosition = offset;
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public void Shoot()
    {
        if (!_canShoot) return;
        _canShoot = false;

        Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction;

        ServiceLocator.Instance.AccessService<AudioService>().PlayAudio(shootSFX);

        if (Physics.Raycast(cameraRay, out RaycastHit cameraHit, maxDistance, layerMask))
        {
            direction = (cameraHit.point - gunTip.position).normalized;
        }
        else
        {
            direction = (cameraRay.GetPoint(maxDistance) - gunTip.position).normalized;
        }

        if (Physics.Raycast(gunTip.position, direction, out RaycastHit hit, maxDistance, layerMask))
        {
            Damage(hit.transform);
        }

        Invoke(nameof(ResetShoot), shootDelay);
    }

    private void Update()
    {
        if (_isShooting)
            Shoot();
    }

    void Damage(Transform target)
    {
        IHealth health = target.gameObject.GetComponent<IHealth>();
        health?.Damage(damage);
    }

    private void ResetShoot()
    {
        _canShoot = true;
    }
}
