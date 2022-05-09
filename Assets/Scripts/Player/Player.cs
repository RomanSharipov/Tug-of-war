using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.Events;
using System;
using RunnerMovementSystem.Examples;

[RequireComponent(typeof(MovementSystem))]
[RequireComponent(typeof(UpgradingVenom))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _throwLassoPoint;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private Transform _enemyContainerPoint;
    [SerializeField] private float _speedAfterEndRoad = 25f;
    [SerializeField] private float _speedAnimationAfterEndRoad = 1.5f;
    [SerializeField] private float _health = 100;
    [SerializeField] private Venom[] _modelsPlayer;
    [SerializeField] private RoadSegment _firstRoad;
    [SerializeField] private RoadSegment _secondRoad;
    [SerializeField] private UpgradingVenomCamera _upgradingVenomCamera;
    [SerializeField] private SwitcherVirtualCamera _switcherVirtualCamera;

    private Transform _transform;
    private MovementSystem _movementSystem;
    private PlayerMovement _playerMovement;
    private UpgradingVenom _upgradingVenom;
    private Venom _currentModelVenom;
    private MouseInput _mouseInput;
    private int _score;

    public Transform Transform => _transform;
    public Transform ThrowLassoPoint => _throwLassoPoint;
    public MovementSystem MovementSystem => _movementSystem;
    public UpgradingVenom UpgradingVenom => _upgradingVenom;
    public float CurrentHealth => _health;
    public Venom CurrentModelVenom => _currentModelVenom;
    public MouseInput MouseInput => _mouseInput;
    public EnemyContainer EnemyContainer => _enemyContainer;
    public int Score => _score;

    public event UnityAction ModelWasChanged;
    public event UnityAction Died;
    public event UnityAction<float> WasTookDamage;
    public event UnityAction<float> WasTookHealth;
    public event UnityAction StoppedMoving;
    public event UnityAction SwitchedRoad;
    public event UnityAction Attacked;
    public event UnityAction Won;
    public event UnityAction WasDefeated;
    public event UnityAction ArrivedOnFinish;

    public void Init()
    {
        _transform = GetComponent<Transform>();
        _upgradingVenom = GetComponent<UpgradingVenom>();
        _upgradingVenomCamera.Init(this);
        _switcherVirtualCamera.Init(this);
        _movementSystem = GetComponent<MovementSystem>();
        _movementSystem.Init(_firstRoad);
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init(_movementSystem,this);
        _upgradingVenom.WasGotNextLevel += ChangeModel;
        _mouseInput = GetComponent<MouseInput>();
        ResetModel();

        foreach (var modelPlayer in _modelsPlayer)
        {
            modelPlayer.Init(this);
        }
    }

    public void AddScore()
    {
        _score++;
    }

    public void TakeDamage(float damage)
    {
        WasTookDamage?.Invoke(damage);
        _health -= damage;
        if (_health == 50)
        {
            Died?.Invoke();
            _mouseInput.enabled = false;
        }
    }

    public void TakeHealth(float health)
    {
        WasTookHealth?.Invoke(health);
        _health += health;
    }

    private void ChangeModel()
    {
        _modelsPlayer[0].gameObject.SetActive(false);
        _modelsPlayer[1].gameObject.SetActive(true);
        _currentModelVenom = _modelsPlayer[1];
        _currentModelVenom.PlayerAnimator.SetSpeed(_modelsPlayer[0].PlayerAnimator.CurrentSpeed);
        ModelWasChanged?.Invoke();
    }

    private void OnDisable()
    {
        _upgradingVenom.WasGotNextLevel -= ChangeModel;
    }

    private void ResetModel()
    {
        _currentModelVenom = _modelsPlayer[0];
        _modelsPlayer[0].gameObject.SetActive(true);
        _modelsPlayer[1].gameObject.SetActive(false);
    }

    public void Attack()
    {
        Attacked.Invoke();
    }

    public void OnFinishedFirstRoad()
    {
        SwitchedRoad?.Invoke();
        MouseInput.enabled = false;
        MovementSystem.Init(_secondRoad);
        MovementSystem.SetOffset(Params.CenterRoad);
    }

    public void Finish()
    {
        ArrivedOnFinish?.Invoke();
    }

    public void Win()
    {
        MovementSystem.MovementOptions.Stop();
        Won?.Invoke();
    }

    public void Lose()
    {
        WasDefeated?.Invoke();
    }
}
