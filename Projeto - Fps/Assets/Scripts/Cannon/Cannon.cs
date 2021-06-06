using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonballPool _cannonballPool = default;
    [SerializeField] private Transform _spawnPoint = default;
    
    [SerializeField] private InteractiveItem _barrelInteractive = default;
    [SerializeField] private TrajectoryController _trajectoryController = default;
    [SerializeField] private ExplosionController _explosionController = default;
    [SerializeField] private CameraShake _cameraShake = default;
    
    private float _firePower = default;
    
    
    private Camera cam = default;
    
    private void Awake()
    {
        InitializeSettings();
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed += OnEnterCannon;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed -= OnEnterCannon;
    }

    private void InitializeSettings()
    {
        _cannonballPool.InitializeCannonballPool();
        _firePower = 200;
    }
    
    private void OnEnterCannon(InputsData inputsData)
    {
        if (_barrelInteractive.OnCannon)
        {
            _trajectoryController.ShowLine = true;
            _trajectoryController.CalculateTrajectory(_spawnPoint,
                _barrelInteractive.transform,_firePower);
            
            if (inputsData.IsAttacking)
            {
                Cannonball currentCannonball = _cannonballPool.GetCannonballFromPool();
                if (currentCannonball != null)
                {
                    _cameraShake.ShakeCamera();
                    
                    currentCannonball.OnColliderAnotherObject += _explosionController.CreateExplosion;
                    currentCannonball.OnColliderAnotherObject += _cannonballPool.DisableCannonball;
                    
                    currentCannonball.gameObject.transform.position = 
                        _spawnPoint.transform.position;
                
                    currentCannonball.gameObject.transform.rotation = 
                        Quaternion.identity;
                    
                    currentCannonball.gameObject.SetActive(true);
                    Vector3 currentSpeed = _trajectoryController.SpeedFinal; 
                    currentCannonball.OnFire(currentSpeed);
                }
            }
        }
        
        else
        {
            _trajectoryController.ShowLine = false;
        }
    }
}
