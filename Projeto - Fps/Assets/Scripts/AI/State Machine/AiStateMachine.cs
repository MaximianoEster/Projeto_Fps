using UnityEngine;

public class AiStateMachine
{
    private IState[] _statesList;
    private EnemyAi _agent;
    private Animator _animator;
    private FpsController _player;
    private StateType _currentState;
    
    public AiStateMachine(EnemyAi agent, Animator animator, FpsController player)
    {
        _agent = agent;
        _animator = animator;
        _player = player;
        
        //Remove latter
        int numStates = System.Enum.GetNames(typeof(StateType)).Length;
        _statesList = new IState[numStates];
    }

    public void RegisterState(IState state)
    {
        int index = (int)state.GetStateId();
        _statesList[index] = state;
    }

    public IState GetState(StateType stateType)
    {
        int index = (int) stateType;
        return _statesList[index];
    }

    public void ChangeState(StateType newState)
    {
        GetState(_currentState)?.Exit(_agent, _animator, _player);
        _currentState = newState;
        
        GetState(_currentState)?.Enter(_agent, _animator, _player);
    }

    public void Update()
    {
        GetState(_currentState)?.Update(_agent, _animator, _player);
    }

    public StateType CurrentState => _currentState;
}
