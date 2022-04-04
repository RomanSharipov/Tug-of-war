using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _speed = 21f;
    [SerializeField] private float _speedReduceDistance = 2f;
    [SerializeField] private float _maxDistanceToPlayer = 15f;
    [SerializeField] private float _targetHeight = 5f;
    [SerializeField] private float _speedStartFly = 3f;
    [SerializeField] private float _stepAddScaleCollider = 0.05f;
    
    
    [SerializeField] private Player _player;
    [SerializeField] private float _currentDistance;

    private List<Enemy> _enemies = new List<Enemy>();
    private Vector3 _direction;
    private Quaternion _targetRotation;
    

    public void Init(Player player)
    {
        _player = player;
    }

    private void FixedUpdate()
    {
        _currentDistance = Vector3.Distance(_player.transform.position, transform.position);
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
            AddEnemy(enemy);
            enemy.transform.SetParent(transform);
            enemy.EnemyAnimator.PullRope();
            enemy.ThrowLassoOnPlayer();
            enemy.SwitchOffMovement();


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
            enemy.EnemyAnimator.HangRope();
        }
    }

    private IEnumerator SmoothStartFly()
    {
        while (transform.position.y < _targetHeight)
        {
            transform.Translate(Vector3.up * _speedStartFly * Time.deltaTime);
            yield return null;
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        _sphereCollider.radius += _stepAddScaleCollider;
    }

    public void ReduceDistanceToPlayer()
    {
        _rigidbody.velocity = transform.forward * _speed * _speedReduceDistance;
    }

    private void MoveToPlayer()
    {
        _speed = _player.MovementSystem.MovementOptions.MoveSpeed;
        
        _direction = _player.transform.position - transform.position;
        _targetRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _rigidbody.velocity = transform.forward * _speed;
    }

    public void ThrowOutStickman(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
