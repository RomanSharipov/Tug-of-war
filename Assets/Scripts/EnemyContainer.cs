using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _speed = 21f;
    [SerializeField] private float _speedReduceDistance = 2f;
    [SerializeField] private float _maxDistanceToPlayer = 25f;
    [SerializeField] private float _targetHeight = 5f;
    [SerializeField] private float _speedStartFly = 3f;
    [SerializeField] private Player _player;
    [SerializeField] private float _currentDistance;

    private List<Enemy> _enemies = new List<Enemy>();
    private Vector3 _direction;
    private Quaternion _targetRotation;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        _currentDistance = Vector3.Distance(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z), transform.position);
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
        transform.position += transform.forward * _speed * _speedReduceDistance *Time.deltaTime;
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
}
