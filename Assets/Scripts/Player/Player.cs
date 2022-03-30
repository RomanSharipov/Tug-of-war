using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.Events;


[RequireComponent(typeof(PlayerAnimator))]
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
    private MovementSystem _movementSystem;
    private PlayerAnimator _playerAnimator;
    private Rigidbody _rigidbody;
    private UpgradingVenom _upgradingVenom;

    public Transform Transform => _transform;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public Transform ThrowLassoPoint => _throwLassoPoint;
    public Transform EndPointLassoJoint => _currentEndPointLassoJoint;
    public MovementSystem MovementSystem => _movementSystem;
    public UpgradingVenom UpgradingVenom => _upgradingVenom;
    public int CurrentHealth => _health;

    public event UnityAction ModelWasChanged;

    public void Init()
    {
        _currentEndPointLassoJoint = _modelsPlayer[0].LassoJointPoint;
        _transform = GetComponent<Transform>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init();
        _rigidbody = GetComponent<Rigidbody>();
        _movementSystem = GetComponent<MovementSystem>();
        _upgradingVenom = GetComponent<UpgradingVenom>();
        _upgradingVenom.Init(this);
        _upgradingVenom.WasGotNextLevel += ChangeModel;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _movementSystem.MovementOptions.ReduceSpeed();
        _playerAnimator.SlowDownAnimation(_stepReduceAnimationSpeed);
    }

    public void TakeHealth(int health)
    {
        _health += health;
    }

    private void ChangeModel()
    {
        _modelsPlayer[0].gameObject.SetActive(false);
        _modelsPlayer[1].gameObject.SetActive(true);
        _currentEndPointLassoJoint = _modelsPlayer[1].LassoJointPoint;
        ModelWasChanged?.Invoke();
    }

    private void OnDisable()
    {
        _upgradingVenom.WasGotNextLevel -= ChangeModel;
    }

}
