using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform gunTip;
    [SerializeField] private float maxDistance;
    [SerializeField] private float shootDelay;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private AudioClip shootSFX;

    private Vector3 hitPoint;

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

        if (Physics.Raycast(cameraRay, out RaycastHit cameraHit, maxDistance))
        {
            direction = (cameraHit.point - gunTip.position).normalized;
        }
        else
        {
            direction = (cameraRay.GetPoint(maxDistance) - gunTip.position).normalized;
        }

        if (Physics.Raycast(gunTip.position, direction, out RaycastHit hit, maxDistance))
        {
            SpawnHitEffect(hit.point, hit.normal);
        }

        Invoke(nameof(ResetShoot), shootDelay);
    }

    private void Update()
    {
        if (_isShooting)
            Shoot();
    }

    void SpawnHitEffect(Vector3 position, Vector3 normal)
    {
        hitPoint = position;
    }

    private void ResetShoot()
    {
        _canShoot = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gunTip.position, hitPoint);
    }
}
