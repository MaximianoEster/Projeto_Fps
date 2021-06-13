using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObjectPool : MonoBehaviour
{
    [SerializeField] private ArrowObjectPoolData _arrowObjectPoolData = default;
    [SerializeField] private Transform _arrowsGroup = default;

    private List<Arrow> _arrowsList = new List<Arrow>();
    
    private int _arrowAmount = default;
    private Arrow _arrowPrefab = default;
    
    public void InitializeArrowPool()
    {
        _arrowAmount = _arrowObjectPoolData.ArrowAmount;
        _arrowPrefab = _arrowObjectPoolData.ArrowPrefab;
        
        CreateArrowPool();
    }

    public void DisableArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrow.transform.SetParent(_arrowsGroup);
        _arrowsList.Add(arrow);
    }

    public Arrow GetArrowFromPool()
    {
        if (_arrowsList.Count > 0)
        {
            Arrow instance = _arrowsList[_arrowsList.Count - 1];
            instance.transform.SetParent(null);
            _arrowsList.Remove(instance);
            return instance;
        }
        return null;
    }
    
    private void CreateArrowPool()
    {
        _arrowsList = new List<Arrow>();
        
        for (int i = 0; i < _arrowAmount; i++)
        {
            Arrow instance = Instantiate(_arrowPrefab);
            instance.transform.SetParent(_arrowsGroup);
            instance.gameObject.SetActive(false);
            _arrowsList.Add(instance);
        }
    }
}
