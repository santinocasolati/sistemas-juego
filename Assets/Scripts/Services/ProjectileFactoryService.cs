using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileFactoryService : BaseService
{
    [SerializeField] private List<Projectile> availableProjectiles = new List<Projectile>();
    private Dictionary<string, GenericPool<GameObject>> projectilePools;

    private void Awake()
    {
        projectilePools = new Dictionary<string, GenericPool<GameObject>>();

        foreach (Projectile projectile in availableProjectiles)
        {
            projectilePools[projectile.name] = new GenericPool<GameObject>(
                parameters => Instantiate(projectile.prefab)
            );
        }
    }

    public GameObject CreateProjectile(string projectileType)
    {
        if (!projectilePools.TryGetValue(projectileType, out var pool))
        {
            throw new System.InvalidOperationException($"Projectile type '{projectileType}' not found in availableProjectiles.");
        }

        return pool.GetObject();
    }

    public void StoreProjectile(string projectileType, GameObject projectile)
    {
        if (projectilePools.TryGetValue(projectileType, out var pool))
        {
            pool.ReleaseObject(projectile);
            projectile.SetActive(false);
        }
    }
}

[System.Serializable]
public struct Projectile
{
    public string name;
    public GameObject prefab;
}
