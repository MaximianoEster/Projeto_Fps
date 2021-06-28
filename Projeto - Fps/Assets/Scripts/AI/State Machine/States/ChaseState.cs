using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private Transform _target;
    private EnemyAi _agent;
    private float _timer;
    private float _maxDistance = 5;
    private float _maxTime = 1;

    private float _chaseStopDistance;
    private float _ChaseSpeed;
    
    public StateType GetStateId()
    {
        return StateType.CHASE;
    }

    public void Enter(EnemyAi agent, Animator animator, FpsController player)
    {
        //Debug.Log("Chase");
        _agent = agent;
        _target = agent.Player;
        _timer = _maxTime;
        
        agent.NavMeshAgent.speed = 5;
        agent.NavMeshAgent.stoppingDistance = 3;
        agent.transform.LookAt(_target);
    }

    public void Update(EnemyAi agent, Animator animator, FpsController player)
    {
        agent.transform.LookAt(_target);
        agent.NavMeshAgent.SetDestination(_target.position);
    }

    public void Exit(EnemyAi agent, Animator animator, FpsController player)
    {
        Debug.Log("Exit Chase State");
      
    }
}
