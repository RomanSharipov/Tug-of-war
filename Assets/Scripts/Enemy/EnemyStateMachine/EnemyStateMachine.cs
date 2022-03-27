using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    [SerializeField] private State _currentState;

    private Enemy _enemy;
    private State[] _states;

    public State Current => _currentState;

    public void Init()
    {
        _enemy = GetComponent<Enemy>();
        _states = GetComponents<State>();

        foreach (var state in _states)
        {
            state.Init(_enemy);
        }
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        State nextState = _currentState.GetNextState();
        
        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Reset(State startState)
    {
        {
            Transit(_firstState);
        }
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter();
        }
    }
}
