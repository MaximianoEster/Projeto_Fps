using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private BowData _bowData = default;
    [SerializeField] private Transform _start = default;
    [SerializeField] private Transform _end = default;
    [SerializeField] private Transform _socket = default;

    private Arrow _currentArrow = default;
    private float _waitTime = 3f;
    private bool _reloaded = false;
    private float _speed = 2000f;

    private void Awake()
    {
        InitializeSettings();
    }

    private void Start()
    {
        StartCoroutine(CreateArrow());
        GameManager.Instance.InputManager.OnAnyKeyPressed += FireArrow;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed -= FireArrow;
    }

    private void InitializeSettings()
    {
        InitializeWeaponSettings();
        _bowData.InitializeBowPool();
    }

    private IEnumerator CreateArrow()
    {
        _reloaded = false;
        yield return new WaitForSeconds(_waitTime);
        
        _currentArrow = _bowData.GetArrowFromPool();
        _currentArrow.gameObject.transform.SetParent(_socket);
        _currentArrow.gameObject.transform.localPosition = new Vector3(0,0,0);
        _currentArrow.gameObject.transform.localEulerAngles = Vector3.zero;
        
        _currentArrow.InitializeArrow();
        _currentArrow.gameObject.SetActive(true);
        _reloaded = true;
    }

    private void FireArrow(InputsData inputsData)
    {
        if (_reloaded && inputsData.IsAttacking)
        {
            _currentArrow.Fire(_speed);
            StartCoroutine(CreateArrow());
        }
    }
    
}
