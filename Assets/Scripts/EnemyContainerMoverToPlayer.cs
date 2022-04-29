using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainerMoverToPlayer : MonoBehaviour
{
    [SerializeField] private float _currentDistance;
    [SerializeField] private float _maxDistanceToPlayer;
    [SerializeField] private float _minDistanceToPlayer;
    [SerializeField] private float _maxDistanceToPlayerForFinish = 39f;
    [SerializeField] private float _minDistanceToPlayerForFinish = 30f;
    [SerializeField] private float _speedReduceDistance;
    [SerializeField] private float _speedAddDistance;
    [SerializeField] private float _stepAddDistanceForUpgradgeVenom;
    [SerializeField] private float _durationRotate = 0.3f;
    [SerializeField] private float _speedRotate = 5f;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedMoveForwardForTurn;

    private Player _player;
    private Coroutine _rotationJob;

    public void Init(Player player)
    {
        _player = player;
        _player.MovementSystem.MovementOptions.SpeedChanged += (float speed) => { _speed = speed; };
        _player.UpgradingVenom.PlayerWasUpgraded += AddDistanceForUpgradgeVenom;
    }

    private void Update()
    {
        _currentDistance = Vector3.Distance(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z), transform.position);

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
        transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void ReduceDistanceToPlayer()
    {
        transform.position += transform.forward * _speedReduceDistance * Time.deltaTime;
    }

    public void AddDistanceToPlayer()
    {
        _speed -= _speedAddDistance;
    }

    private IEnumerator SmoothRotateLeft()
    {
        float oldSpeed = _speed;
        _speed = 0;
        float timePassed = 0;
        while (timePassed < _durationRotate)
        {

            transform.RotateAround(_player.transform.position, Vector3.up, _speedRotate * Time.deltaTime);
            transform.position += transform.forward * _speedMoveForwardForTurn * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }
        _speed = oldSpeed;
    }

    private IEnumerator SmoothRotateRight()
    {
        float timePassed = 0;
        float oldSpeed = _speed;
        _speed = 0;
        while (timePassed < _durationRotate)
        {

            transform.RotateAround(_player.transform.position, Vector3.up, -_speedRotate * Time.deltaTime);
            transform.position += transform.forward * _speedMoveForwardForTurn * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }
        _speed = oldSpeed;
    }

    public void FlyLeft()
    {
        if (_rotationJob != null)
        {
            StopCoroutine(_rotationJob);
        }
        _rotationJob = StartCoroutine(SmoothRotateLeft());
    }

    public void FlyRight()
    {
        if (_rotationJob != null)
        {
            StopCoroutine(_rotationJob);
        }
        _rotationJob = StartCoroutine(SmoothRotateRight());
    }

    private void OnDisable()
    {
        _player.MovementSystem.MovementOptions.SpeedChanged -= (float speed) => { _speed = speed; };
    }

    public void SetDistanceToPlayerOnFinish()
    {
        _maxDistanceToPlayer = _maxDistanceToPlayerForFinish;
        _minDistanceToPlayer = _minDistanceToPlayerForFinish;
    }
}