using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLassoState : State
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minDistanceToPlayer;
    [SerializeField] private float _maxDistanceToPlayer;
    [SerializeField] private GameObject _targetPointInParentTemplate;
    [SerializeField] private float _radiusSphereOverlast;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] [Range(0, 1f)] private float _distanceBetweenStickmans;

    private Collider[] _colliders;
    [SerializeField] private float _currentDistanceToPlayer;
    private GameObject _targetPointInParent;
    [SerializeField] private float _speed;

    private void OnEnable()
    {
        Enemy.ThrowLasso();
        Enemy.EnemyAnimator.PullRope();
        _targetPointInParent = Instantiate(_targetPointInParentTemplate, Enemy.Player.Transform);
        _targetPointInParent.transform.localPosition = new Vector3(0, 0, 0);
        _targetPointInParent.name = $"Target Point {gameObject.name}";
        _speed = Enemy.Player.MovementSystem.MovementOptions.MoveSpeed;
    }

    private void Update()
    {
        _currentDistanceToPlayer = Vector3.Distance(Enemy.Transform.position, Enemy.Player.Transform.position);
        _speed = Enemy.Player.MovementSystem.MovementOptions.MoveSpeed;


        if (IsEnemyNearby())
        {
            ResetForce();
            Vector3 offsetTargetPoint = (_colliders[0].transform.localPosition - transform.localPosition).normalized;


            _targetPointInParent.transform.localPosition -= offsetTargetPoint * _distanceBetweenStickmans;
        }

        if (_currentDistanceToPlayer < _minDistanceToPlayer)
        {
            return;
        }

        Enemy.EnemyMovement.MoveTo(_targetPointInParent.transform.position, Enemy.Player.MovementSystem.MovementOptions.MoveSpeed, _rotationSpeed);
    }

    public bool IsEnemyNearby()
    {
        int oldLayer = gameObject.layer;
        gameObject.layer = 0;
        _colliders = Physics.OverlapSphere(transform.position, _radiusSphereOverlast, _enemy);
        gameObject.layer = oldLayer;
        return _colliders.Length > 0;
    }

    public void ResetForce()
    {
        Enemy.Rigidbody.velocity = Vector3.zero;
        Enemy.Rigidbody.angularVelocity = Vector3.zero;
    }
}
