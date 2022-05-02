using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _targetHeight = 5f;
    [SerializeField] private float _speedStartFly = 3f;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyContainerMoverToPlayer _enemyContainerMoverToPlayer;

    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _rotationJob;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _enemyContainerMoverToPlayer = new EnemyContainerMoverToPlayer();
        _enemyContainerMoverToPlayer.Init(_player,this);
        _player.SwitchedRoad += OnStartedMoving;
    }

    private void Update()
    {
        _enemyContainerMoverToPlayer.Move();
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
        _enemyContainerMoverToPlayer.SetDistanceToPlayerOnFinish();
        StartFly();

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
        enemy.transform.SetParent(transform);
    }

    public bool IsEnemyInContainer(Enemy enemy)
    {
        return _enemies.Contains(enemy);
    }

    private void OnDisable()
    {
        _player.SwitchedRoad += OnStartedMoving;
        _enemyContainerMoverToPlayer.OnDisable();
    }


    public void FlyLeft()
    {
        _enemyContainerMoverToPlayer.FlyLeft();
    }

    public void FlyRight()
    {
        _enemyContainerMoverToPlayer.FlyRight();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}