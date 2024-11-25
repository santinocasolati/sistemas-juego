using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    [SerializeField] private string projectileName;

    public override void Shoot()
    {
        if (!_canShoot) return;

        base.Shoot();

        GameObject projectile = ServiceLocator.Instance.AccessService<ProjectileFactoryService>().CreateProjectile(projectileName);
        Vector3 direction = (Camera.main.transform.position + Camera.main.transform.forward * 100f) - gunTip.position;
        direction.Normalize();

        projectile.GetComponent<ProjectileController>().Setup(direction);
        projectile.transform.position = gunTip.position;
        projectile.transform.rotation = gunTip.rotation;

        projectile.SetActive(true);
    }
}
