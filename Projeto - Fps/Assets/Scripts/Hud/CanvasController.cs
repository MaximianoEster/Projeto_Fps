using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Action<WeaponType> UpdateWeapon;
    
    [SerializeField] private WeaponWheelController _weaponWheel = default;
    private WeaponType _currentWeaponSelected = default;
    
    private void Awake()
    {
        InitializeUiElements();
    }
    
    private void Start()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed += OpenWeaponWheel;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed -= OpenWeaponWheel;
    }

    private void InitializeUiElements()
    {
        _weaponWheel.InitializeSettings();
        _weaponWheel.OnWeaponSelected += WeaponSelected;
    }

    private void OpenWeaponWheel(InputsData inputsdata)
    {
        if (inputsdata.OpenWeaponWheel)
        {
            _weaponWheel.gameObject.SetActive(true);
        }
        else
        {
            _weaponWheel.gameObject.SetActive(false);
        }
    }

    private void WeaponSelected(WeaponType type)
    {
        _currentWeaponSelected = type;
        Debug.Log(type);
        UpdateWeapon?.Invoke(_currentWeaponSelected);
    }
}
