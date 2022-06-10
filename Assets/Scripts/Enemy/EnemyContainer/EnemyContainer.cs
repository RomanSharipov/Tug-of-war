using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _targetScaleX = 0.5f;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyContainerMover _enemyContainerMover;

    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _rotationJob;
    private Transform _transform;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _enemyContainerMover = new EnemyContainerMover();
        _enemyContainerMover.Init(_player,this);
        _player.SwitchedRoad += OnStartedMoving;
        _player.Won += OnPlayerWon;
    }

    private void OnPlayerWon()
    {
        foreach (var enemy in _enemies.ToArray())
        {
            enemy.TakeOffLasso();
        }
    }

    private void Update()
    {
        _enemyContainerMover.Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (IsEnemyInContainer(enemy))
                return;

            enemy.ThrowLassoOnPlayer();
        }
    }

    private void OnStartedMoving()
    {
        _enemyContainerMover.ParamsDistance.SetDistanceToPlayerOnFinish();
        StartFly();
    }

    
    public void StartFly()
    {
        _sphereCollider.isTrigger = false;
        _enemyContainerMover.StartFly();

        foreach (var enemy in _enemies)
        {
            enemy.StartFly();
            _enemyContainerMover.SetRandomHeight(enemy);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.transform.SetParent(transform);
    }

    public bool IsEnemyInContainer(Enemy enemy)
    {
        return _enemies.Contains(enemy);
    }

    private void OnDisable()
    {
        _player.SwitchedRoad += OnStartedMoving;
        _enemyContainerMover.OnDisable();
    }

    public void FlyLeft()
    {
        _enemyContainerMover.FlyLeft();
    }

    public void FlyRight()
    {
        _enemyContainerMover.FlyRight();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}