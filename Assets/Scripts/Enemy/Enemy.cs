using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RunnerMovementSystem;


[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(MovementSystem))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CableProceduralCurve _cableProceduralCurve;
    [SerializeField] private int _damage;

    private EnemyMovement _enemyMovement;
    private EnemyContainer _enemyContainer;
    private MovementSystem _movementOnWay;
    private Transform _transform;
    private EnemyStateMachine _enemyStateMachine;
    private EnemyAnimator _enemyAnimator;
    private Rigidbody _rigidbody;
    private Player _player;
    private Vector3 _direction;
    private Quaternion _targetRotation;


    public Player Player => _player;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public Transform Transform => _transform;
    public EnemyAnimator EnemyAnimator => _enemyAnimator;
    public Rigidbody Rigidbody => _rigidbody;
    public MovementSystem MovementOnWay => _movementOnWay;
    public EnemyContainer EnemyContainer => _enemyContainer;

    public void Init(Player player,EnemyContainer enemyContainer, RoadSegment _roadSegment)
    {
        _enemyContainer = enemyContainer;
        
        _player = player;
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _enemyMovement = new EnemyMovement();
        _enemyMovement.Init(_transform);
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyStateMachine.Init();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAnimator.Init();
        _movementOnWay = GetComponent<MovementSystem>();
        _movementOnWay.Init(_roadSegment);
        _player.ModelWasChanged += SwitchEndPointLasso;
    }

    public void ThrowLassoOnPlayer()
    {
        LookOnTarget();
        _cableProceduralCurve.SetEndPoint(Player.CurrentModelVenom.LassoJointPoint);
        _cableProceduralCurve.gameObject.SetActive(true);
        Player.TakeDamage(_damage);
    }

    public void SwitchEndPointLasso()
    {
        _cableProceduralCurve.SetEndPoint(Player.CurrentModelVenom.LassoJointPoint);
    }

    private void OnDisable()
    {
        _player.ModelWasChanged -= SwitchEndPointLasso;
    }

    public void SwitchOffMovement()
    {
        _enemyStateMachine.Current.enabled = false;
        _enemyStateMachine.enabled = false;

        Destroy(_rigidbody);
        _movementOnWay.enabled = false;
    }

    public void LookOnTarget()
    {
        _direction = Player.transform.position - transform.position;
        _targetRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void TakeOffLasso()
    {
        _cableProceduralCurve.gameObject.SetActive(false);
        transform.parent = null;
        gameObject.AddComponent<Rigidbody>();
    }

}
