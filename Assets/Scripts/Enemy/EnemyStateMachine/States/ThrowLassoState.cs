using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLassoState : State
{
    [SerializeField] private float _radiusSphereOverlast;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private Quaternion _targetRotation;
    private Collider[] _colliders;

    private void FixedUpdate()
    {
        MoveOnPoint();

        if (IsEnemyNearby())
        {
            Enemy.transform.SetParent(_enemyContainer.transform);
            Enemy.EnemyAnimator.PullRope();
            Enemy.ThrowLasso();
            Enemy.SwitchOffMovement();
            _enemyContainer.AddEnemy(Enemy);
        }
    }

    private bool IsEnemyNearby()
    {
        int oldLayer = gameObject.layer;
        gameObject.layer = 0;
        _colliders = Physics.OverlapSphere(transform.position, _radiusSphereOverlast, _enemy);
        gameObject.layer = oldLayer;
        return _colliders.Length > 0;
    }

    private void MoveOnPoint()
    {
        _direction = _enemyContainer.transform.position - transform.position;
        _targetRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Enemy.Rigidbody.velocity = transform.forward * _speed;
    }

}
