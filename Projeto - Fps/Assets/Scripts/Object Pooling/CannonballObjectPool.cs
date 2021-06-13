using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballObjectPool : MonoBehaviour
{
    [SerializeField] private CannonballPoolData cannonballPoolData = default;
    [SerializeField] private Transform _cannonballGroup = default;
    
    private Cannonball _cannonballPrefab;
    private int _cannonballAmount;
    private List<Cannonball> _cannonballList = new List<Cannonball>();
    
    public void InitializeCannonballPool()
    {
        _cannonballPrefab = cannonballPoolData.CannonballPrefab;
        _cannonballAmount = cannonballPoolData.CannonballAmount;
        
        CreateCannonballPool();
    }
    
    public Cannonball GetCannonballFromPool()
    {
        if (_cannonballList.Count > 0)
        {
            Cannonball instance = _cannonballList[_cannonballList.Count - 1];
            instance.transform.SetParent(null);
            _cannonballList.Remove(instance);
            return instance;
        }
        return null;
    }

    public void DisableCannonball(Cannonball cannonball)
    {
        cannonball.gameObject.SetActive(false);
        cannonball.transform.SetParent(_cannonballGroup);
        _cannonballList.Add(cannonball);
    }
    
    private void CreateCannonballPool()
    {
        _cannonballList = new List<Cannonball>();
        
        for (int i = 0; i < _cannonballAmount; i++)
        {
            Cannonball temp = Instantiate(_cannonballPrefab);
            temp.transform.SetParent(_cannonballGroup);
            temp.gameObject.SetActive(false);
            _cannonballList.Add(temp);
        }
    }
}
