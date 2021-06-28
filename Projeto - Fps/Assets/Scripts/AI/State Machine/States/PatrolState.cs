using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private int _currentIndex;
    private EnemyAi _agent;
    private bool _onDestination;
    public StateType GetStateId()
    {
        return StateType.PATROL;
    }

    public void Enter(EnemyAi agent, Animator animator, FpsController player)
    {
        _agent = agent;
        agent.NavMeshAgent.speed = 2;
        _onDestination = false;
    }
    
    public void Update(EnemyAi agent, Animator animator, FpsController player)
    {
      
        if ( agent.NavMeshAgent.remainingDistance < 1 )
        {
            _currentIndex = Random.Range(0, GameManager.Instance.CheckPoints.Count);
            agent.NavMeshAgent.SetDestination(GameManager.Instance.CheckPoints[_currentIndex].position);
            _onDestination = false;
        }
        animator.SetFloat(AnimationParameters.Speed, agent.NavMeshAgent.velocity.magnitude);
    }

    public void Exit(EnemyAi agent, Animator animator, FpsController player)
    {
        agent.NavMeshAgent.SetDestination(agent.Player.position);
    }
}
