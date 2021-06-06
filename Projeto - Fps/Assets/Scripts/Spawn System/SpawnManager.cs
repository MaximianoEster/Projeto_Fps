using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemiesObjectPool _enemiesObjectPool = default;
    [SerializeField] private List<Transform> _spawnPointsList = default;
    [SerializeField] private Transform _destination = default;

    private void Awake()
    {
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        _enemiesObjectPool.InitializeEnemiesPool();
    }

    private void SpawnEnemy()
    {
        EnemyController currentEnemy = _enemiesObjectPool.GetRandomEnemyFromPool();
        if (currentEnemy != null)
        {
            currentEnemy.transform.SetParent(null);
            currentEnemy.transform.position = GetRandomSpawnPoint().position;
            currentEnemy.gameObject.SetActive(true);
            currentEnemy.SetDestination(_destination);
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int randomSpawnPointIndex = Random.Range(0, _spawnPointsList.Count);
        return _spawnPointsList[randomSpawnPointIndex];
    }
}
