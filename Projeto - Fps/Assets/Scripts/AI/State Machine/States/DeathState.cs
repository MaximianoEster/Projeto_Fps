using UnityEngine;

public class DeathState : IState
{
    private float _timer = 4f;
    public StateType GetStateId()
    {
        return StateType.DEATH;
    }

    public void Enter(EnemyAi agent, Animator animator, FpsController player)
    {
        agent.NavMeshAgent.SetDestination(agent.transform.position);
        animator.SetBool(AnimationParameters.IsDead, true);
    }

    public void Update(EnemyAi agent, Animator animator, FpsController player)
    {
        DisableEnemy(agent);
    }

    public void Exit(EnemyAi agent, Animator animator, FpsController player)
    {
        agent.NavMeshAgent.SetDestination(agent.transform.position);
    }

    private void DisableEnemy(EnemyAi agent)
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            agent.gameObject.SetActive(false);
            /*
            Vfx deathEffect = GameManager.Instance.ObjectPoolManager.VfxObjectPool.GetVfxFromPool(VfxType.LIGHT_DEATH);
            deathEffect.transform.position = agent.CenterPoint.position;
            deathEffect.transform.rotation = Quaternion.identity;
            deathEffect.gameObject.SetActive(true);
            */
        }
    }
}
