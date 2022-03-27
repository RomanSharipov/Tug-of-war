using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerState : State
{
    [SerializeField] private int _speed;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _targetPoint;

    private void Update()
    {
        _targetPoint = Random.insideUnitSphere + Enemy.Player.ThrowLassoPoint.position;

        //Enemy.EnemyMovement.MoveTo(_targetPoint, _speed, _rotationSpeed);
        Enemy.EnemyMovement.MoveTo(Enemy.Player.ThrowLassoPoint.position, _speed, _rotationSpeed);
    }


    private void OnEnable()
    {
        Enemy.EnemyAnimator.Run();
    }
}
