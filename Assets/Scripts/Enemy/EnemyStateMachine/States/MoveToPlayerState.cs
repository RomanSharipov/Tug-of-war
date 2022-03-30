using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerState : State
{
    [SerializeField] private int _speed;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        Enemy.EnemyMovement.MoveTo(Enemy.Player.ThrowLassoPoint.position, _speed, _rotationSpeed);
    }

    private void OnEnable()
    {
        Enemy.EnemyAnimator.Run();
    }
}
