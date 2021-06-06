using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxObjectPool : MonoBehaviour
{
    [SerializeField] private VfxObjectPoolData _vfxObjectPoolData = default;
    [SerializeField] private Transform _vfxGroup = default;
    
    private Dictionary<VfxType, List<Vfx>> 
        _vfxDictionary = new Dictionary<VfxType, List<Vfx>>();

    private int _amountVfx = 10;

    private void Start()
    {
        AddListToDictionary(_vfxObjectPoolData.ExplosionCannonVfx);
    }
    
    private void AddListToDictionary(Vfx objectData)
    {
        for (int i = 0; i < _amountVfx; i++)
        {
            Vfx vfxInstance = Instantiate(objectData);
            vfxInstance.gameObject.SetActive(false);
            vfxInstance.transform.SetParent(_vfxGroup);
            vfxInstance.InitializeSettings();
            AddEnemyToDictionary(vfxInstance);
        }
    }

    private void AddEnemyToDictionary(Vfx vfxData)
    {
        if (_vfxDictionary.ContainsKey(vfxData.Type))
        {
            _vfxDictionary[vfxData.Type].Add(vfxData);
        }
        else
        {
            _vfxDictionary.Add(vfxData.Type, new List<Vfx>());
            _vfxDictionary[vfxData.Type].Add(vfxData);
        }
    }

    public Vfx GetVfxFromPool(VfxType type)
    {
        if (_vfxDictionary[type].Count > 0)
        {
            Vfx currentVfx = _vfxDictionary[type][_vfxDictionary[type].Count - 1];
            currentVfx.transform.SetParent(null);
            _vfxDictionary[type].Remove(currentVfx);
            return currentVfx;
        }

        return null;
    }
}
