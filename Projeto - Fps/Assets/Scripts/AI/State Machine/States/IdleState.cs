using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float maxDistance = 2;
    public StateType GetStateId()
    {
        return StateType.IDLE;
    }

    public void Enter(EnemyAi agent, Animator animator, FpsController player)
    {
       return;
    }

    public void Update(EnemyAi agent, Animator animator, FpsController player)
    {
        Vector3 targetDirection = agent.Player.position - agent.transform.position;
        if (targetDirection.magnitude > maxDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;
        targetDirection.Normalize();

        float dotProduct = Vector3.Dot(targetDirection, agentDirection);
        
       
    }

    public void Exit(EnemyAi agent, Animator animator, FpsController player)
    {
        return;
    }

}
