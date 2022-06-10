using System;
using System.Collections;
using UnityEngine;


[Serializable]
public class EnemyContainerMover 
{
    [SerializeField] private float _currentDistance;
    [SerializeField] private float _speedStartFly = 0.02f;
    [SerializeField] private float _targetHeight = 9f;
    [SerializeField] private float _speedRotate = 30f;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedMoveForwardWhileTurning = 15f;
    [SerializeField] private float _minHeight = 3;
    [SerializeField] private float _maxHeight = 4;
    [SerializeField] private float _speedSettingRandomHeight = 2;

    private Player _player;
    private Coroutine _rotationJob;
    private Transform _transform;
    private MonoBehaviour _enemyContainerOnScene;
    private ParamsDistance _paramsDistance;

    public ParamsDistance ParamsDistance => _paramsDistance;

    public void Init(Player player, MonoBehaviour monoBehaviour)
    {
        _enemyContainerOnScene = monoBehaviour;
        _player = player;
        _player.MovementSystem.MovementOptions.SpeedChanged += (float speed) => { _speed = speed; };
        _paramsDistance = new ParamsDistance();
        _player.UpgradingVenom.PlayerWasUpgraded += _paramsDistance.AddDistanceForUpgradgeVenom;
        _transform = _enemyContainerOnScene.transform;
    }

    public void Move()
    {
        _currentDistance = Vector3.Distance(new Vector3(_player.transform.position.x, _transform.position.y, _player.transform.position.z), _transform.position);

        if (_currentDistance < _paramsDistance.MinDistanceToPlayer)
        {
            AddDistanceToPlayer();
        }

        MoveToPlayer();

        if (_currentDistance > _paramsDistance.MaxDistanceToPlayer)
        {
            ReduceDistanceToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        _transform.LookAt(new Vector3(_player.transform.position.x, _transform.position.y, _player.transform.position.z));
        _transform.position += _transform.forward * _speed * Time.deltaTime;
    }

    public void ReduceDistanceToPlayer()
    {
        _transform.position += _transform.forward * _paramsDistance.SpeedReduceDistance * Time.deltaTime;
    }

    public void AddDistanceToPlayer()
    {
        _speed -= _paramsDistance.SpeedAddDistance;
    }

    public IEnumerator SmoothRotateLeft()
    {
        float oldSpeed = _speed;
        _speed = 0;

        while (_enemyContainerOnScene.enabled)
        {
            _transform.RotateAround(_player.transform.position, Vector3.up, _speedRotate * Time.deltaTime);
            _transform.position += _transform.forward * _speedMoveForwardWhileTurning * Time.deltaTime;
            yield return null;
        }
        _speed = oldSpeed;
    }

    public IEnumerator SmoothRotateRight()
    {
        float oldSpeed = _speed;
        _speed = 0;
        while (_enemyContainerOnScene.enabled)
        {
            _transform.RotateAround(_player.transform.position, Vector3.up, -_speedRotate * Time.deltaTime);
            _transform.position += _transform.forward * _speedMoveForwardWhileTurning * Time.deltaTime;
            yield return null;
        }
        _speed = oldSpeed;
    }

    public void OnDisable()
    {
        _player.MovementSystem.MovementOptions.SpeedChanged -= (float speed) => { _speed = speed; };
    }

    public void FlyLeft()
    {
        if (_rotationJob != null)
        {
            _enemyContainerOnScene.StopCoroutine(_rotationJob);
        }
        _rotationJob = _enemyContainerOnScene.StartCoroutine(SmoothRotateLeft());
    }

    public void FlyRight()
    {
        if (_rotationJob != null)
        {
            _enemyContainerOnScene.StopCoroutine(_rotationJob);
        }
        _rotationJob = _enemyContainerOnScene.StartCoroutine(SmoothRotateRight());
    }

    private IEnumerator SmoothStartFly()
    {
        while (_enemyContainerOnScene.transform.position.y < _targetHeight)
        {
            _enemyContainerOnScene.transform.position += Vector3.up * _speedStartFly * Time.deltaTime;
            yield return null;
        }
    }

    public void StartFly()
    {
        _enemyContainerOnScene.StartCoroutine(SmoothStartFly());
    }

    private IEnumerator SmoothSetRandomHeight(Enemy enemy)
    {
        Vector3 targetPosition = new Vector3();
        targetPosition.Set(enemy.transform.localPosition.x, UnityEngine.Random.Range(_minHeight, _maxHeight), enemy.transform.localPosition.z);

        while (enemy.transform.localPosition.y < targetPosition.y)
        {
            enemy.transform.localPosition = Vector3.MoveTowards(enemy.transform.localPosition, targetPosition, _speedSettingRandomHeight * Time.deltaTime);
            yield return null;
        }
    }

    public void SetRandomHeight(Enemy enemy)
    {
        _enemyContainerOnScene.StartCoroutine(SmoothSetRandomHeight(enemy));
    }
}