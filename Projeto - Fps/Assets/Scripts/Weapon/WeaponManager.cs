using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponsList = default;
    [SerializeField] private Transform _weaponGroup = default;

    private Dictionary<WeaponType, Weapon> _weaponDicitionary = 
        new Dictionary<WeaponType, Weapon>();

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        for (int i = 0; i < _weaponsList.Count; i++)
        {
            Weapon instance = Instantiate(_weaponsList[i]);
            instance.transform.SetParent(_weaponGroup);
            instance.gameObject.SetActive(false);
            AddWeaponToDictionary(instance);
        }
    }

    private void EnableWeapon(WeaponType type)
    {
        for (int i = 0; i < _weaponDicitionary.Count; i++)
        {
            _weaponDicitionary[(WeaponType) i].gameObject.SetActive(false);
        }
        
        _weaponDicitionary[type].gameObject.SetActive(true);
    }

    private void AddWeaponToDictionary(Weapon weaponData)
    {
        if (!_weaponDicitionary.ContainsKey(weaponData.Type))
        {
           _weaponDicitionary.Add(weaponData.Type, weaponData);
        }
        else
        {
            return;
        }
    }
    
    public void ChangeWeapon(WeaponType type)
    {
        /*
        for (int i = 0; i < _weaponDicitionary.Count; i++)
        {
            _weaponDicitionary[(WeaponType) i].gameObject.SetActive(false);
        }
        */
        
        _weaponDicitionary[type].gameObject.SetActive(true);
    }

    public void EnableWeapon()
    {
        if (_weaponsList != null)
        {
            for (int i = 0; i < _weaponsList.Count; i++)
            {
                _weaponsList[i].gameObject.SetActive(false);
            }

            //_weaponsList[_weaponIndex].gameObject.SetActive(true);
        }
    }
}
