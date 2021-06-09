using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObjectPool : MonoBehaviour
{
    [SerializeField] private ArrowObjectPoolData _arrowObjectPoolData = default;
    [SerializeField] private Transform _arrowsGroup = default;
    
    private Dictionary<ArrowType, List<Arrow>> _arrowDictionary = 
        new Dictionary<ArrowType, List<Arrow>>();
    
    private int _arrowAmount = default;

    public void InitializeArrowPool()
    {
        _arrowAmount = _arrowObjectPoolData.ArrowAmount;
        AddListToDictionary(_arrowObjectPoolData.ArrowsPrefabsList);
    }

    public void AddArrowToListAgain(Arrow currentArrow)
    {
        AddArrowToDicitionary(currentArrow);
    }

    public Arrow GetArrowFromPool(ArrowType arrowType)
    {
        if (_arrowDictionary[arrowType].Count > 0)
        {
            Arrow instance = _arrowDictionary[arrowType][_arrowDictionary[arrowType].Count - 1];
            _arrowDictionary[arrowType].Remove(instance);
            return instance;
        }

        return null;
    }
    
    private void AddListToDictionary(List<Arrow> arrowList)
    {
        for (int i = 0; i < _arrowObjectPoolData.ArrowsPrefabsList.Count; i++)
        {
            for (int c = 0; c < _arrowAmount; c++)
            {
                Arrow arrowInstance = Instantiate( arrowList[i]);
                arrowInstance.InitializeArrow();
                AddArrowToDicitionary(arrowInstance);
            }
        }
    }

    private void AddArrowToDicitionary(Arrow arrowData)
    {
        arrowData.gameObject.SetActive(false);
        arrowData.transform.SetParent(_arrowsGroup);
        
        if (_arrowDictionary.ContainsKey(arrowData.Type))
        {
            _arrowDictionary[arrowData.Type].Add(arrowData);
        }
        else
        {
            _arrowDictionary.Add(arrowData.Type, new List<Arrow>());
            _arrowDictionary[arrowData.Type].Add(arrowData);
        }
    }
}
