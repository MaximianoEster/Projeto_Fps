using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour, IDetectable
{
    [Header("Enemy AI Settings")]
    [SerializeField] protected StateType _initialState = default;
    [SerializeField] protected NavMeshAgent _navMeshAgent = default;
    [SerializeField] protected Transform _player = default;
    [SerializeField] protected Animator _animator = default;
    [SerializeField] protected FpsController _fpsController = default;
    [SerializeField] protected Transform _centerPoint = default;
    [SerializeField] private EnemyData _enemyData = default;

    protected bool _canAttack = false;
    protected float _healthAmount = default;
    protected AiStateMachine _stateMachine = default;
    protected EnemyType _type = default;

    public virtual void InitializeEnemy()
    {
        _type = _enemyData.EnemyType;
        _healthAmount = _enemyData.HealthAmount;
    }
    
    public NavMeshAgent NavMeshAgent
    {
        get => _navMeshAgent;
        set => _navMeshAgent = value;
    }

    public bool CanAttack
    {
        get => _canAttack;
        set => _canAttack = value;
    }

    public Transform CenterPoint => _centerPoint;
    public Transform Player => _player;
    public AiStateMachine StateMachine => _stateMachine;
    public EnemyType Type => _type;
}
