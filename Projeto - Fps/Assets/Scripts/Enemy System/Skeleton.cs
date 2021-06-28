using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyAi
{
    [Header("Skeleton Settings")]
    [SerializeField] private FieldOfView _fov = default;
    [SerializeField] private Transform _spawnPoint = default;
    [SerializeField] private HealthController _healthController = default;
    
    private bool _isInitialized = false;
    
    private void Awake()
    {
        InitializeEnemy();
    }
    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        _healthController.InitializeHealth(5);
        CreateStateMachine();
        SubscribeEvents();
        _isInitialized = true;
      
    }

    private void EnableAttack()
    {
        GameObject currentBomb = GameManager.Instance.ObjectPoolManager.GetObjectFromPool(ItemType.SHOCK_BOMB);
        if (currentBomb.TryGetComponent(out ShockBomb bomb))
        {
            if (!bomb.IsInitialized)
            {
                bomb.InitializeShockBomb();
            }
            
            bomb.transform.SetParent(null);
            bomb.transform.position = _spawnPoint.position;
            bomb.transform.rotation = Quaternion.identity;
            
            Vector3 finalSpeed = (_player.position - _spawnPoint.position).normalized;
            bomb.gameObject.SetActive(true);
            bomb.LaunchBomb(finalSpeed);
            
        }
    }
    
    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    
    private void Update()
    {
        _fov.Timer();
        _stateMachine.Update();
    }
    
    private void CreateStateMachine()
    {
        _stateMachine = new AiStateMachine(this, _animator, _fpsController);
        
        _stateMachine.RegisterState(new PatrolState());
        _stateMachine.RegisterState(new DeathState());
        _stateMachine.RegisterState(new BombAttackState());
        
        _stateMachine.ChangeState(_initialState);
    }
    
    private void SubscribeEvents()
    {
        _fov.OnObjDetectable += AttackPlayer;
        _fov.OnObjUndetectable += ReturnToPatrol;
        _healthController.OnHealthIsOver += EnemyDeath;
    }
    
    private void UnsubscribeEvents()
    {
        _fov.OnObjDetectable -= AttackPlayer;
        _fov.OnObjUndetectable -= ReturnToPatrol;
        _healthController.OnHealthIsOver += EnemyDeath;
    }
    
    private void AttackPlayer()
    {
        _stateMachine.ChangeState(StateType.ATTACK_BOMB);
    }

    private void ReturnToPatrol()
    {
        _stateMachine.ChangeState(StateType.PATROL);
    }

    private void EnemyDeath()
    {
        _stateMachine.ChangeState(StateType.DEATH);
    }
    
    public Transform SpawnPoint => _spawnPoint;
    public FieldOfView Fov => _fov;
}
