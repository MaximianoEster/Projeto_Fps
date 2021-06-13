using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponsList = default;
    [SerializeField] private Transform _weaponGroup = default;
    [SerializeField] private InteractionHandler _interactionHandler = default;
    
    private Dictionary<WeaponType, Weapon> _weaponDicitionary = 
        new Dictionary<WeaponType, Weapon>();

    private Weapon _currentWeaponEquiped = default;
    private bool _isAim = false;
    
    private void Start()
    {
        InitializeWeaponSettings();
        GameManager.Instance.InputManager.OnAttackPerformed += CurrentWeaponAction;
        GameManager.Instance.InputManager.OnAimPerformed += CheckAiming;
    }

    public void EquipWeapon(WeaponType type)
    {
        if (_currentWeaponEquiped != null)
        {
            _currentWeaponEquiped.gameObject.SetActive(false);
        }
        
        _currentWeaponEquiped = _weaponDicitionary[type];
        _currentWeaponEquiped.gameObject.SetActive(true);
    }

    public void InitializeWeaponSettings()
    {
        InitializeDictionary();
        EquipWeapon(WeaponType.SIMPLE_BOW);
    }
    
    private void InitializeDictionary()
    {
        for (int i = 0; i < _weaponsList.Count; i++)
        {
            Weapon instance = Instantiate(_weaponsList[i]);
            instance.transform.position = _weaponGroup.position;
            instance.transform.rotation = _weaponGroup.rotation;
            instance.transform.SetParent(_weaponGroup);
            instance.gameObject.SetActive(false);
            AddWeaponToDictionary(instance);
        }
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

    private void CurrentWeaponAction()
    {
        if (!_interactionHandler.IsInteracting && _isAim)
        {
            _currentWeaponEquiped.OnAttack();
        }
    }

    private void CheckAiming()
    {
        _isAim = !_isAim;
        _currentWeaponEquiped.SetCurrentPosition(_isAim);
    }
    
    public Weapon CurrentWeaponEquiped => _currentWeaponEquiped;
}
