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
    [SerializeField] private Quaternion _maxRotationRight;
    [SerializeField] private Quaternion _maxRotationLeft;
    [SerializeField] private Quaternion _current;
    
    

    private Player _player;
    private List<Enemy> _enemies = new List<Enemy>();
    
    private SwipeInput _swipeInput;
    private Coroutine _rotationJob;

    public void Init(Player player, SwipeInput swipeInput)
    {
        _player = player;
        _swipeInput = swipeInput;
        _swipeInput.SwipedRight += FlyRight;
        _swipeInput.SwipedLeft += FlyLeft;
    }

    private void Update()
    {
        _current = transform.rotation;
        _currentDistance = Vector3.Distance(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z), transform.position);
        
        if (_currentDistance < _minDistanceToPlayer)
        {
            return;
        }

        MoveToPlayer();

        if (_currentDistance > _maxDistanceToPlayer)
        {
            ReduceDistanceToPlayer();
        }
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

    private void MoveToPlayer()
    {
        _speed = _player.MovementSystem.CurrentSpeed;
        transform.LookAt(new Vector3(_player.transform.position.x,transform.position.y, _player.transform.position.z) );
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void ThrowOutStickman(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private IEnumerator SmoothRotateLeft()
    {
        while (transform.rotation.y < _maxRotationLeft.y)
        {
            transform.RotateAround(_player.transform.position, Vector3.up, _speedRotate * Time.deltaTime);
            yield return null;

        }
    }

    private IEnumerator SmoothRotateRight()
    {
        while (transform.rotation.y > _maxRotationRight.y)
        {
            transform.RotateAround(_player.transform.position, Vector3.up, -_speedRotate * Time.deltaTime);
            yield return null;

        }
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