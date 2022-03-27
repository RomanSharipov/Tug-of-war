using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Enemy _enemy;

    abstract public bool NeedTransit  { set; get; }

    public State TargetState => _targetState;
    public Enemy Enemy => _enemy;

    public void Init(Enemy enemy)
    {
        NeedTransit = false;
        
        _enemy = enemy;
    }
}
