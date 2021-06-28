using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttackState : IState
{
    private float delay = 2;
    private Transform _spawnPoint;
    public StateType GetStateId()
    {
        return StateType.ATTACK_BOMB;
    }

    public void Enter(EnemyAi agent, Animator animator, FpsController player)
    {
        agent.NavMeshAgent.SetDestination(agent.transform.position);
        animator.SetBool(AnimationParameters.IsAttacking, true);
    }

    public void Update(EnemyAi agent, Animator animator, FpsController player)
    {
        agent.transform.LookAt(player.transform);
        return;
    }

    public void Exit(EnemyAi agent, Animator animator, FpsController player)
    {
        animator.SetBool(AnimationParameters.IsAttacking, false);
    }
}
