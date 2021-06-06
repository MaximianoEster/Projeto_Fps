using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData = default;
    [SerializeField] private NavMeshAgent _navMeshAgent = default;
    [SerializeField] private AnimatorController _animatorController = default;

    private float _damage = default;
    private float _health = default;
    private EnemyType _type = default;
    
    public void InitializeEnemy()
    {
        _damage = _enemyData.DamageAmount;
        _health = _enemyData.HealthAmount;
        _type = _enemyData.EnemyType;
    } 
    
    public void SetDestination(Transform destiny)
    {
        _navMeshAgent.SetDestination(destiny.position);
    }
    
    private void Update()
    {
        _animatorController.LocomotionAnimation(_navMeshAgent.velocity.magnitude);
    }
    
    public EnemyType Type => _type;
}
