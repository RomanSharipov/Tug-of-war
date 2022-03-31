using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.Events;
using System;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementSystem))]
[RequireComponent(typeof(UpgradingVenom))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _throwLassoPoint;
    [SerializeField] private int _stepReduceSpeed;
    [SerializeField] private float _stepReduceAnimationSpeed = 0.1f;
    [SerializeField] private int _health = 100;
    [SerializeField] private Venom[] _modelsPlayer;

    private Transform _currentEndPointLassoJoint;
    private Transform _transform;
    private float _startSpeed;
    private MovementSystem _movementSystem;
    private Rigidbody _rigidbody;
    private UpgradingVenom _upgradingVenom;
    private Venom _currentModelVenom;

    public Transform Transform => _transform;
    public Transform ThrowLassoPoint => _throwLassoPoint;
    public Transform EndPointLassoJoint => _currentEndPointLassoJoint;
    public MovementSystem MovementSystem => _movementSystem;
    public UpgradingVenom UpgradingVenom => _upgradingVenom;
    public int CurrentHealth => _health;
    public Venom CurrentModelVenom => _currentModelVenom;

    public event UnityAction ModelWasChanged;
    public event UnityAction Died;

    public void Init()
    {
        _currentEndPointLassoJoint = _modelsPlayer[0].LassoJointPoint;
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _movementSystem = GetComponent<MovementSystem>();
        _upgradingVenom = GetComponent<UpgradingVenom>();
        _upgradingVenom.Init(this);
        _upgradingVenom.WasGotNextLevel += ChangeModel;
        _startSpeed = MovementSystem.MovementOptions.MoveSpeed;
        _currentModelVenom = _modelsPlayer[0];

        foreach (var modelPlayer in _modelsPlayer)
        {
            modelPlayer.Init(this);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health == 0)
        {
            Died?.Invoke();
        }
        _movementSystem.MovementOptions.ReduceSpeed(GetTotalValue(damage));
        CurrentModelVenom.PlayerAnimator.SlowDownAnimation(_stepReduceAnimationSpeed);
    }

    public void TakeHealth(int health)
    {
        _health += health;
        MovementSystem.MovementOptions.AddSpeed(GetTotalValue(health));
    }

    private void ChangeModel()
    {
        _modelsPlayer[0].gameObject.SetActive(false);
        _modelsPlayer[1].gameObject.SetActive(true);
        _currentEndPointLassoJoint = _modelsPlayer[1].LassoJointPoint;
        _currentModelVenom = _modelsPlayer[1];
        ModelWasChanged?.Invoke();
    }

    private void OnDisable()
    {
        _upgradingVenom.WasGotNextLevel -= ChangeModel;
    }

    private float GetTotalValue(int percent)
    {
        return _startSpeed / 100 * percent;
    }

}
