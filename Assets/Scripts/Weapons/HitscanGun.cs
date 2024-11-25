using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : BaseWeapon
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask layerMask;

    public override void Shoot()
    {
        if (!_canShoot) return;

        base.Shoot();

        Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction;

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
    }

    void Damage(Transform target)
    {
        IHealth health = target.gameObject.GetComponent<IHealth>();
        health?.Damage(damage);
    }
}
