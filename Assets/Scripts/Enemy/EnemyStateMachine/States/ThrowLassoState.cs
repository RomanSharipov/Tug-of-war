using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLassoState : State
{
    [SerializeField] private float _radiusSphereOverlast;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private Quaternion _targetRotation;
    private Collider[] _colliders;

    private void FixedUpdate()
    {
        MoveOnPoint();

        //if (IsEnemyNearby())
        //{
        //    Enemy.ThrowLassoOnPlayer();
        //}
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
        transform.LookAt(new Vector3(Enemy.EnemyContainer.transform.position.x, transform.position.y, Enemy.EnemyContainer.transform.position.z));
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy anotherEnemy))
        {
            if (Enemy.EnemyContainer.IsEnemyInContainer(Enemy))
                return;

            if (Enemy.EnemyContainer.IsEnemyInContainer(anotherEnemy))
                Enemy.ThrowLassoOnPlayer();
        }
    }

}
