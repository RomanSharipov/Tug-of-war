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

    public void Init(Player player)
    {
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
        _player.ModelWasChanged += SwitchEndPointLasso;
    }

    public void ThrowLasso()
    {
        LookOnTarget();
        _cableProceduralCurve.SetEndPoint(Player.EndPointLassoJoint);
        _cableProceduralCurve.gameObject.SetActive(true);
        Player.TakeDamage(_damage);
    }

    public void SwitchEndPointLasso()
    {
        _cableProceduralCurve.SetEndPoint(Player.EndPointLassoJoint);
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
}
