using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint = default;

    [SerializeField] private InteractiveItem _barrelInteractive = default;
    [SerializeField] private TrajectoryController _trajectoryController = default;
    [SerializeField] private ExplosionController _explosionController = default;
    [SerializeField] private CameraShake _cameraShake = default;

    [SerializeField] private float _firePower = default;
    
    private void Start()
    {
        GameManager.Instance.InputManager.OnAttackPerformed += FireCannon;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAttackPerformed -= FireCannon;
    }

    private void Update()
    {
        CalculateAndShowTrajectory();
    }
    
    private void CalculateAndShowTrajectory()
    {
        if (_barrelInteractive.OnCannon)
        {
            _trajectoryController.CalculateTrajectory(_spawnPoint,
                _barrelInteractive.transform, _firePower);
            _trajectoryController.ShowLine = true;
        }

        else
        {
            _trajectoryController.ShowLine = false;
        }
    }

    private void FireCannon()
    {
        if (_barrelInteractive.OnCannon)
        {
            /*
            Cannonball currentCannonball = cannonballObjectPool.GetCannonballFromPool();
            if (currentCannonball != null)
            {
                _cameraShake.ShakeCamera();

                currentCannonball.OnColliderAnotherObject += _explosionController.CreateExplosion;
                currentCannonball.OnColliderAnotherObject += cannonballObjectPool.DisableCannonball;

                currentCannonball.gameObject.transform.position =
                    _spawnPoint.transform.position;

                currentCannonball.gameObject.transform.rotation =
                    Quaternion.identity;

                currentCannonball.gameObject.SetActive(true);
                Vector3 currentSpeed = _trajectoryController.Speed;
                currentCannonball.OnFire(currentSpeed);
                
            }
            */
        }
    }
}
