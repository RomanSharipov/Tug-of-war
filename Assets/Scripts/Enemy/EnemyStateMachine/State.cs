using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    private Transform _transform;
    private Enemy _enemy;

    public Enemy Enemy => _enemy;

    public void Init(Enemy enemy)
    {
        _transform = GetComponent<Transform>();
        _enemy = enemy;
    }

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;
            
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(_enemy);
            }
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }
        return null;
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }
}
