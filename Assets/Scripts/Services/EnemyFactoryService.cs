using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFactoryService : BaseService
{
    [SerializeField] private List<Enemy> availableEnemies = new List<Enemy>();
    private Dictionary<string, GenericPool<GameObject>> enemyPools;

    private void Awake()
    {
        enemyPools = new Dictionary<string, GenericPool<GameObject>>();

        foreach (Enemy enemy in availableEnemies)
        {
            enemyPools[enemy.name] = new GenericPool<GameObject>(
                parameters => Instantiate(enemy.prefab)
            );
        }
    }

    public GameObject CreateEnemy(string enemyType)
    {
        if (!enemyPools.TryGetValue(enemyType, out var pool))
        {
            throw new System.InvalidOperationException($"Enemy type '{enemyType}' not found in availableEnemies.");
        }

        GameObject enemy = pool.GetObject();
        enemy.GetComponent<IHealth>().Reset();

        return enemy;
    }

    public void StoreEnemy(string enemyType, GameObject enemy)
    {
        if (enemyPools.TryGetValue(enemyType, out var pool))
        {
            pool.ReleaseObject(enemy);
            enemy.SetActive(false);
        }
    }
}

[System.Serializable]
public struct Enemy
{
    public string name;
    public GameObject prefab;
}
