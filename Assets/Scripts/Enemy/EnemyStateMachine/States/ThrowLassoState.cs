using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLassoState : State
{
    [SerializeField] private float _speed;

    private void Update()
    {
        MoveOnPoint();
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
            bool alreadyThrowLassoOnPlayer = Enemy.EnemyContainer.IsEnemyInContainer(Enemy);

            if (alreadyThrowLassoOnPlayer)
                return;

            if (Enemy.EnemyContainer.IsEnemyInContainer(anotherEnemy))
                Enemy.ThrowLassoOnPlayer();
        }
    }

}
