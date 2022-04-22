using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _speed = 21f;
    [SerializeField] private float _speedReduceDistance = 2f;
    [SerializeField] private float _speedRotate = 5f;
    [SerializeField] private float _maxDistanceToPlayer = 25f;
    [SerializeField] private float _minDistanceToPlayer = 15f;
    [SerializeField] private float _targetHeight = 5f;
    [SerializeField] private float _speedStartFly = 3f;
    [SerializeField] private float _currentDistance;
    [SerializeField] private float _durationRotate;
    [SerializeField] private Player _player;
    [SerializeField] private SwipeInput _swipeInput;

    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _rotationJob;
    private EnemyContainerMoverToPlayer _enemyContainerMoverToPlayer; 

    private void OnEnable()
    {
        //_player = player;
        //_swipeInput = swipeInput;
        _swipeInput.SwipedRight += FlyRight;
        _swipeInput.SwipedLeft += FlyLeft;
        _enemyContainerMoverToPlayer = GetComponent<EnemyContainerMoverToPlayer>();
        _enemyContainerMoverToPlayer.Init(_player);
        _player.StartedMoving += StartFly;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.ThrowLassoOnPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeOffLasso();
        }
    }

    public void StartFly()
    {
        _sphereCollider.enabled = false;
        StartCoroutine(SmoothStartFly());

        foreach (var enemy in _enemies)
        {
            enemy.StartFly();
        }
    }

    private IEnumerator SmoothStartFly()
    {
        while (transform.position.y < _targetHeight)
        {
            transform.position += Vector3.up * _speedStartFly * Time.deltaTime;
            yield return null;
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void ReduceDistanceToPlayer()
    {
        transform.position += transform.forward  * _speedReduceDistance *Time.deltaTime;
    }

    public void ThrowOutStickman(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private IEnumerator SmoothRotateLeft()
    {
        _enemyContainerMoverToPlayer.enabled = false;
        float timePassed = 0;
        while (timePassed < _durationRotate)
        {
            transform.RotateAround(_player.transform.position, Vector3.up, _speedRotate * Time.deltaTime);
            transform.position += transform.forward * _speed * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }
        _enemyContainerMoverToPlayer.enabled = true;
    }

    private IEnumerator SmoothRotateRight()
    {
        float timePassed = 0;
        _enemyContainerMoverToPlayer.enabled = false;
        while (timePassed < _durationRotate)
        {
            transform.RotateAround(_player.transform.position, Vector3.up, -_speedRotate * Time.deltaTime);
            transform.position += transform.forward * _speed * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }
        _enemyContainerMoverToPlayer.enabled = true;
    }

    private void FlyLeft()
    {
        if (_rotationJob != null)
        {
            StopCoroutine(_rotationJob);
        }
        _rotationJob = StartCoroutine(SmoothRotateLeft());
    }

    private void FlyRight()
    {
        if (_rotationJob != null)
        {
            StopCoroutine(_rotationJob);
        }
        _rotationJob = StartCoroutine(SmoothRotateRight());
    }

    private void OnDisable()
    {
        _swipeInput.SwipedRight -= FlyRight;
        _swipeInput.SwipedLeft -= FlyLeft;
    }
}
