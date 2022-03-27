using UnityEngine;
using RunnerMovementSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _throwLassoPoint;
    [SerializeField] private int _stepReduceSpeed;
    [SerializeField] private float _stepReduceAnimationSpeed = 0.1f;
    [SerializeField] private Transform _lassoJointPoint;

    private Transform _transform;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private MovementSystem _movementSystem;
    private PlayerAnimator _playerAnimator;
    private Rigidbody _rigidbody;

    public Transform Transform => _transform;
    public PlayerInput PlayerInput => _playerInput;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public Transform ThrowLassoPoint => _throwLassoPoint;
    public Transform LassoJointPoint => _lassoJointPoint;
    public MovementSystem MovementSystem => _movementSystem;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init();
        _rigidbody = GetComponent<Rigidbody>();
        _movementSystem = GetComponent<MovementSystem>();
    }



    public void TakeLasso()
    {
        _movementSystem.MovementOptions.ReduceSpeed();
        _playerAnimator.SlowDownAnimation(_stepReduceAnimationSpeed);
    }
}
