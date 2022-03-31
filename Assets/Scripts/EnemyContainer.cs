using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private LayerMask _enemy;
    
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _speed;
    [SerializeField] private float _targetHeight;
    [SerializeField] private float _speedStartFly;
    [SerializeField] private float _stepAddScaleCollider = 0.1f;
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;

    private List<Enemy> _enemies = new List<Enemy>();
    private Vector3 _direction;
    private float _currentDisctance;
    private Quaternion _targetRotation;

    private void FixedUpdate()
    {
        _speed = _player.MovementSystem.MovementOptions.MoveSpeed;
        _currentDisctance = Vector3.Distance(_target.position , transform.position);
        _direction = _target.position - transform.position;
        _targetRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Rigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            AddEnemy(enemy);
            enemy.transform.SetParent(transform);
            enemy.EnemyAnimator.PullRope();
            enemy.ThrowLasso();
            enemy.SwitchOffMovement();
        }
    }

    public void StartFly()
    {
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _targetHeight, transform.position.z), _speedStartFly * Time.deltaTime);
            yield return null;
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        _sphereCollider.radius += _stepAddScaleCollider;
    }

}
