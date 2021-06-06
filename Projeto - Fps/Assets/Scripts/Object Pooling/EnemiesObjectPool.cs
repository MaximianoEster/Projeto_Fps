using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesObjectPool : MonoBehaviour
{
    [SerializeField] private EnemiesObjectPoolData _enemiesObjectPoolData = default;
    [SerializeField] private Transform _enemiesGroup = default;
    
    private int _enemiesAmount = default;
    
    private Dictionary<EnemyType, List<EnemyController>> 
        _enemiesDictionary = new Dictionary<EnemyType, List<EnemyController>>();

    public void InitializeEnemiesPool()
    {
        _enemiesAmount = _enemiesObjectPoolData.EnemiesAmount;
        InitializeDictionary();
    }
    
    public EnemyController GetRandomEnemyFromPool()
    {
        int indexEnemyClass = Random.Range(0, _enemiesDictionary.Count);
        
        if (_enemiesDictionary[(EnemyType) indexEnemyClass].Count > 0)
        {
            EnemyController instance = _enemiesDictionary[(EnemyType) indexEnemyClass]
                [_enemiesDictionary[(EnemyType) indexEnemyClass].Count - 1];
            
            _enemiesDictionary[(EnemyType)indexEnemyClass].Remove(instance);
            return instance;
        }

        return null;
    }

    public void AddEnemyAtPool(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(_enemiesGroup);
        AddEnemyToDictionary(enemy);
    }
    
    private void InitializeDictionary()
    {
        AddListToDictionary(_enemiesObjectPoolData._skeletonsList, _enemiesGroup);
        AddListToDictionary(_enemiesObjectPoolData._knightList, _enemiesGroup);
    }
   
    private void AddListToDictionary(List<EnemyController> objectData, Transform enemiesGroup)
    {
        for (int i = 0; i < objectData.Count; i++)
        {
            for (int enemies = 0; enemies < _enemiesAmount; enemies++)
            {
                EnemyController enemyInstance = Instantiate(objectData[i]);
           
                enemyInstance.InitializeEnemy();
                enemyInstance.gameObject.SetActive(false);
                enemyInstance.transform.SetParent(enemiesGroup);
                AddEnemyToDictionary(enemyInstance);
            }
        }
    }

    private void AddEnemyToDictionary(EnemyController enemyData)
    {
        if (_enemiesDictionary.ContainsKey(enemyData.Type))
        {
            _enemiesDictionary[enemyData.Type].Add(enemyData);
        }
        else
        {
            _enemiesDictionary.Add(enemyData.Type, new List<EnemyController>());
            _enemiesDictionary[enemyData.Type].Add(enemyData);
        }
    }
}
