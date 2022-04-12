using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.Events;
using System;
using RunnerMovementSystem.Examples;
using System.Collections;

[RequireComponent(typeof(MovementSystem))]
[RequireComponent(typeof(UpgradingVenom))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private const float CenterRoad = 0;
    [SerializeField] private Transform _throwLassoPoint;
    [SerializeField] private Transform _enemyContainerPoint;
    [SerializeField] private float _stepReduceSpeed;
    
    [SerializeField] private float _speedAfterEndRoad = 25f;
    [SerializeField] private float _speedAnimationAfterEndRoad = 1.5f;
    [SerializeField] private int _health = 100;
    [SerializeField] private Venom[] _modelsPlayer;

    
    private Transform _transform;
    private float _startSpeed;
    private MovementSystem _movementSystem;
    
    private PlayerMovement _playerMovement;
    
    
    private UpgradingVenom _upgradingVenom;
    private Venom _currentModelVenom;
    private MouseInput _mouseInput;
    private RoadSegment _secondRoad;

    public Transform Transform => _transform;
    public PlayerMovement PlayerMovement => _playerMovement;
    
    public Transform ThrowLassoPoint => _throwLassoPoint;
    public Transform EnemyContainerPoint => _enemyContainerPoint;

    public MovementSystem MovementSystem => _movementSystem;
    public UpgradingVenom UpgradingVenom => _upgradingVenom;
    public int CurrentHealth => _health;
    public Venom CurrentModelVenom => _currentModelVenom;
    
    public MouseInput MouseInput => _mouseInput;

    public event UnityAction ModelWasChanged;
    public event UnityAction Died;
    public event UnityAction StoppedMoving;
    public event UnityAction StartedMoving;
    public event UnityAction FinishedFirstRoad;

    public void Init(RoadSegment firstRoad, RoadSegment secondRoad)
    {
        _transform = GetComponent<Transform>();
         
        _movementSystem = GetComponent<MovementSystem>();
        _movementSystem.Init(firstRoad);
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init(_movementSystem);
        _upgradingVenom = GetComponent<UpgradingVenom>();
        //_upgradingVenom.Init(this);
        _upgradingVenom.WasGotNextLevel += ChangeModel;
        _startSpeed = MovementSystem.MovementOptions.MoveSpeed;
        _mouseInput = GetComponent<MouseInput>();
        ResetModel();
        _secondRoad = secondRoad;

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
            _mouseInput.enabled = false;
        }
        _movementSystem.MovementOptions.ReduceSpeed(GetTotalValue(damage));
        CurrentModelVenom.PlayerAnimator.ReduceSpeedAnimation(_speedAnimationAfterEndRoad);
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
        
        _currentModelVenom = _modelsPlayer[1];
        _currentModelVenom.PlayerAnimator.SetSpeed(_modelsPlayer[0].PlayerAnimator.CurrentSpeed);
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

    private void ResetModel()
    {
        _currentModelVenom = _modelsPlayer[0];
        _modelsPlayer[0].gameObject.SetActive(true);
        _modelsPlayer[1].gameObject.SetActive(false);
    }

    public void StopMove()
    {
        MovementSystem.MovementOptions.Stop();
        StoppedMoving?.Invoke();
    }

    public void StartMove()
    {
        MovementSystem.MovementOptions.SetSpeed(_speedAfterEndRoad);
        CurrentModelVenom.PlayerAnimator.SetSpeed(_speedAnimationAfterEndRoad);
        StartedMoving?.Invoke();
    }

    public void wSwitchRoad()
    {
        StartMove();
        MovementSystem.Init(_secondRoad);
        MovementSystem.SetOffset(CenterRoad);
        
    }

    public void OnFinishedFirstRoad()
    {
        //StopMove();
        //MouseInput.enabled = false;
        //FinishedFirstRoad?.Invoke();
        //wSwitchRoad();
        StartCoroutine(SwitchRoad());
    }

    private IEnumerator SwitchRoad()
    {
        StopMove();
        MouseInput.enabled = false;
        FinishedFirstRoad?.Invoke();
        yield return new WaitForSeconds(1f);
        StartMove();
        StartedMoving?.Invoke();
        MovementSystem.Init(_secondRoad);
        MovementSystem.SetOffset(CenterRoad);
    }

}
