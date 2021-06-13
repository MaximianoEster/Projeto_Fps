using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Action<WeaponType> UpdateWeapon;
    
    [SerializeField] private WeaponWheelController _weaponWheel = default;
    
    private WeaponType _currentWeaponSelected = default;
    private bool _isWeaponWheelOpen = false;
    
    private void Awake()
    {
        InitializeUiElements();
    }
    
    private void Start()
    {
        GameManager.Instance.InputManager.OnTabPerformed += OpenWeaponWheel;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnTabPerformed += OpenWeaponWheel;
    }

    private void InitializeUiElements()
    {
        _weaponWheel.InitializeSettings();
        _weaponWheel.OnWeaponSelected += WeaponSelected;
    }

    private void OpenWeaponWheel()
    {
        _isWeaponWheelOpen = !_isWeaponWheelOpen;
        if (_isWeaponWheelOpen)
        {
            _weaponWheel.gameObject.SetActive(_isWeaponWheelOpen);
        }
        else
        {
            _weaponWheel.gameObject.SetActive(_isWeaponWheelOpen);
        }
    }

    private void WeaponSelected(WeaponType type)
    {
        _currentWeaponSelected = type;
        UpdateWeapon?.Invoke(_currentWeaponSelected);
    }
}
