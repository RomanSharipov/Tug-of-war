using UnityEngine;
using RunnerMovementSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _throwLassoPoint;
    [SerializeField] private int _stepReduceSpeed;
    [SerializeField] private float _stepReduceAnimationSpeed = 0.1f;
    [SerializeField] private Transform _lassoJointPoint;
    [SerializeField] private int _health = 100;

    private Transform _transform;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private MovementSystem _movementSystem;
    private PlayerAnimator _playerAnimator;
    private Rigidbody _rigidbody;
    private UpgradingVenom _upgradingVenom;

    public Transform Transform => _transform;
    public PlayerInput PlayerInput => _playerInput;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public Transform ThrowLassoPoint => _throwLassoPoint;
    public Transform LassoJointPoint => _lassoJointPoint;
    public MovementSystem MovementSystem => _movementSystem;
    public UpgradingVenom UpgradingVenom => _upgradingVenom;
    public int CurrentHealth => _health;

    public void Init()
    {
        _transform = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init();
        _rigidbody = GetComponent<Rigidbody>();
        _movementSystem = GetComponent<MovementSystem>();
        _upgradingVenom = GetComponent<UpgradingVenom>();
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

}
