using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    
    [SerializeField] private float _speedReduceDistance = 2f;


    [SerializeField] private float _targetHeight = 5f;
    [SerializeField] private float _speedStartFly = 3f;
    
    
    [SerializeField] private Player _player;
    

    private List<Enemy> _enemies = new List<Enemy>();
    
    private EnemyContainerMoverToPlayer _enemyContainerMoverToPlayer;

    private void Start()
    {
        _enemyContainerMoverToPlayer = GetComponent<EnemyContainerMoverToPlayer>();
        _enemyContainerMoverToPlayer.Init(_player);
        _player.StartedMoving += StartFly;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeOffLasso();
        }
    }

    public void StartFly()
    {
        _sphereCollider.isTrigger = false;
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
        transform.position += transform.forward * _speedReduceDistance * Time.deltaTime;
    }

    public void ThrowOutStickman(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }



    public bool IsEnemyInContainer(Enemy enemy)
    {
        return _enemies.Contains(enemy);
    }

    public void StopMoveForSeconds(float seconds)
    {
        StartCoroutine(StopMoveForSecondsJob(seconds));
    }

    private IEnumerator StopMoveForSecondsJob(float seconds)
    {
        _enemyContainerMoverToPlayer.enabled = false;
        yield return new WaitForSeconds(seconds);
        _enemyContainerMoverToPlayer.enabled = true;
    }
}