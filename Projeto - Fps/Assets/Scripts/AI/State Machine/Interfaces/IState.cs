using UnityEngine;

public interface IState
{
    StateType GetStateId();
    void Enter(EnemyAi agent, Animator animator, FpsController player);
    void Update(EnemyAi agent, Animator animator, FpsController player);
    void Exit(EnemyAi agent, Animator animator, FpsController player);
}
