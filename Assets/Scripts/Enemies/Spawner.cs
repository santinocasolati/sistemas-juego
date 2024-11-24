using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;
    [SerializeField] private float _yPos;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;
    [SerializeField] private List<string> enemies = new();

    private void Start()
    {
        StartCoroutine(WaitRandomDelay());
    }

    private IEnumerator WaitRandomDelay()
    {
        float randomDelay = Random.Range(_minSpawnTime, _maxSpawnTime);
        yield return new WaitForSeconds(randomDelay);

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomZ = Random.Range(_minZ, _maxZ);
        Vector3 spawnPosition = new Vector3(randomX, _yPos, randomZ);

        GameObject enemy = ServiceLocator.Instance.AccessService<EnemyFactoryService>().CreateEnemy(enemies[Random.Range(0, enemies.Count)]);
        enemy.transform.position = spawnPosition;
        enemy.SetActive(true);

        StartCoroutine(WaitRandomDelay());
    }
}
