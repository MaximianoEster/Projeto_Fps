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

    private Transform _currentSpawnPoint = default;
    private Vfx _currentVfx = default;
    
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
            _currentSpawnPoint = GetRandomSpawnPoint();
            
            EnableSpawnVfx();
            currentEnemy.transform.SetParent(null);
            currentEnemy.transform.position = _currentSpawnPoint.position;
            currentEnemy.gameObject.SetActive(true);
            currentEnemy.SetDestination(_destination);
        }
    }

    private void EnableSpawnVfx()
    {
        _currentVfx = GameManager.Instance.VfxManager.GetVfxFromPool(VfxType.SKELETON_SPAWN);
        _currentVfx.transform.position = _currentSpawnPoint.position;
        _currentVfx.gameObject.SetActive(true);
    }

    private Transform GetRandomSpawnPoint()
    {
        int randomSpawnPointIndex = Random.Range(0, _spawnPointsList.Count);
        return _spawnPointsList[randomSpawnPointIndex];
    }
}
