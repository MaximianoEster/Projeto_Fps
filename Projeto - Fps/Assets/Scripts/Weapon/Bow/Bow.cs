using System;
using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;

public class Bow : Weapon
{
    [Header("Bow Settings")]
    [SerializeField] private BowData _bowData = default;
    
    [Space, Header("Arrows Settings")]
    [SerializeField] private Transform _socket = default;
    
    private float _timeToReload = default;
    private float _arrowLaunchForce = default;
    private bool _reloaded = false;
    private Arrow _currentArrow = default;
    
    private void Awake()
    {
        InitializeBowSettings();
    }

    private void Start()
    {
        StartCoroutine(CreateArrow());
        OnAttackPerformed += FireArrow;
    }

    private void OnDisable()
    {
        OnAttackPerformed -= FireArrow;
    }
    
    private void OnEnable()
    {
        _anchor.position = _defaultPosition.position;
    }

    public void InitializeBowSettings()
    {
        InitializeWeaponSettings();
        _timeToReload = _bowData.TimeToReload;
        _arrowLaunchForce = _bowData.ArrowLaunchForce;
    }

    private IEnumerator CreateArrow()
    {
        _reloaded = false;
        yield return new WaitForSeconds(_timeToReload);
        SetNewArrow();
    }

    private void SetNewArrow()
    {
        /*
        _currentArrow = GetArrow();
        _currentArrow.gameObject.transform.SetParent(_socket);
        _currentArrow.gameObject.transform.localPosition = Vector3.zero;
        _currentArrow.gameObject.transform.localRotation = Quaternion.identity;
        
        _currentArrow.InitializeArrow();
        _currentArrow.gameObject.SetActive(true);
        _reloaded = true;
        */
    }

    private Arrow GetArrow()
    {
        GameObject arrow = null;
        if (_weaponType == WeaponType.SIMPLE_BOW)
        {
            arrow = GameManager.Instance.ObjectPoolManager.GetObjectFromPool(ItemType.SIMPLE_ARROW);
        }
        else if(_weaponType == WeaponType.FLAME_BOW)
        {
            arrow = GameManager.Instance.ObjectPoolManager.GetObjectFromPool(ItemType.SIMPLE_ARROW);
        }

        if (arrow.TryGetComponent(out Arrow newArrow))
        {
            return newArrow;
        }

        return null;
    }
    
    private void FireArrow()
    {
        if (_reloaded)
        {
            _currentArrow.transform.SetParent(null);
            _currentArrow.Fire(_arrowLaunchForce);
            
            StartCoroutine(CreateArrow());
        }
    }
}
