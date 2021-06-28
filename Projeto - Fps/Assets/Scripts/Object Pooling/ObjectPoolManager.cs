using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPoolData> _objectPoolDataList = default;
    [SerializeField] private Transform _container = default;
    
    private Dictionary<ItemType, List<GameObject>> _poolDictionary = new Dictionary<ItemType, List<GameObject>>();
    
    public void InitializeObjectPool()
    {
        foreach (ObjectPoolData objectPoolData in _objectPoolDataList)
        {
            AddPoolObjectToDictionary(objectPoolData);
        }
    }
    
    public void ReturnToPool(ItemType type, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(_container, true);
        _poolDictionary[type].Add(obj);
    }

    public GameObject GetObjectFromPool(ItemType type)
    {
        if (_poolDictionary[type].Count > 0)
        {
            GameObject instance = _poolDictionary[type][_poolDictionary[type].Count - 1];
            _poolDictionary[type].Remove(instance);
            instance.transform.SetParent(null);
            return instance;
        }

        return null;
    }
    
    private void AddPoolObjectToDictionary(ObjectPoolData data)
    {
        for (int i = 0; i < data.PrefabAmount; i++)
        {
            GameObject instance = Instantiate(data.Prefab, _container, true);
            instance.SetActive(false);
            
            if (_poolDictionary.ContainsKey(data.itemType))
            {
                _poolDictionary[data.itemType].Add(instance);
            }
            else
            {
                _poolDictionary.Add(data.itemType, new List<GameObject>());
                _poolDictionary[data.itemType].Add(instance);
            }
        }
    }
}
