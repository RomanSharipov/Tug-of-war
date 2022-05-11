using System;
using System.Collections;
using UnityEngine;


[Serializable]
public class EnemyContainerMover 
{
    [SerializeField] private float _currentDistance;
    [SerializeField] private float _maxDistanceToPlayer = 15;
    [SerializeField] private float _minDistanceToPlayer = 5;
    [SerializeField] private float _maxDistanceToPlayerForFinish = 39f;
    [SerializeField] private float _minDistanceToPlayerForFinish = 30f;
    [SerializeField] private float _speedReduceDistance = 15f;
    [SerializeField] private float _speedAddDistance = 0.3f;
    [SerializeField] private float _stepAddDistanceForUpgradgeVenom = 1.5f;
    [SerializeField] private float _speedStartFly = 3f;
    [SerializeField] private float _targetHeight = 5f;
    [SerializeField] private float _speedRotate = 30f;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedMoveForwardWhileTurning = 15f;

    private Player _player;
    private Coroutine _rotationJob;
    private Transform _transform;
    private MonoBehaviour _enemyContainerOnScene;

    public void Init(Player player, MonoBehaviour monoBehaviour)
    {
        _enemyContainerOnScene = monoBehaviour;
        _player = player;
        _player.MovementSystem.MovementOptions.SpeedChanged += (float speed) => { _speed = speed; };
        _player.UpgradingVenom.PlayerWasUpgraded += AddDistanceForUpgradgeVenom;
        _transform = _enemyContainerOnScene.transform;
    }

    public void Move()
    {
        _currentDistance = Vector3.Distance(new Vector3(_player.transform.position.x, _transform.position.y, _player.transform.position.z), _transform.position);

        if (_currentDistance < _minDistanceToPlayer)
        {
            AddDistanceToPlayer();
        }

        MoveToPlayer();

        if (_currentDistance > _maxDistanceToPlayer)
        {
            ReduceDistanceToPlayer();
        }
    }

    private void AddDistanceForUpgradgeVenom()
    {
        _maxDistanceToPlayer += _stepAddDistanceForUpgradgeVenom;
        _minDistanceToPlayer += _stepAddDistanceForUpgradgeVenom;
    }


    private void MoveToPlayer()
    {
        _transform.LookAt(new Vector3(_player.transform.position.x, _transform.position.y, _player.transform.position.z));
        _transform.position += _transform.forward * _speed * Time.deltaTime;
    }

    public void ReduceDistanceToPlayer()
    {
        _transform.position += _transform.forward * _speedReduceDistance * Time.deltaTime;
    }

    public void AddDistanceToPlayer()
    {
        _speed -= _speedAddDistance;
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

    public void SetDistanceToPlayerOnFinish()
    {
        _maxDistanceToPlayer = _maxDistanceToPlayerForFinish;
        _minDistanceToPlayer = _minDistanceToPlayerForFinish;
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


}