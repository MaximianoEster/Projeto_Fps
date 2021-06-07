using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    [SerializeField] private VfxObjectPoolData _vfxObjectPoolData = default;
    [SerializeField] private Transform _vfxGroup = default;
    
    private Dictionary<VfxType, List<Vfx>> 
        _vfxDictionary = new Dictionary<VfxType, List<Vfx>>();
    
    private int _amountVfx = default;
    
    private void Start()
    {
        InitializeSettings();
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
    
    public void DisableVfx(Vfx currentVfx)
    {
        AddVfxToDictionary(currentVfx);
    }
    
    public void InitializeSettings()
    {
        _amountVfx = _vfxObjectPoolData.VfxAmount;
        AddListToDictionary(_vfxObjectPoolData.VfxPrefabsList);
    }
    
    private void AddListToDictionary(List<Vfx> vfxList)
    {
        for (int i = 0; i < vfxList.Count; i++)
        {
            for (int vfx = 0; vfx < _amountVfx; vfx++)
            {
                Vfx vfxInstance = Instantiate(_vfxObjectPoolData.VfxPrefabsList[i]);
                
                vfxInstance.DisableVfx += DisableVfx;
                vfxInstance.InitializeSettings();
                AddVfxToDictionary(vfxInstance);
            }
        } 
    }

    private void AddVfxToDictionary(Vfx vfxData)
    {
        vfxData.gameObject.SetActive(false);
        vfxData.transform.SetParent(_vfxGroup);
        
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
}
