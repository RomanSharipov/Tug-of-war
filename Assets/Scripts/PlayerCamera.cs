using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraDistanceStepOffset;
    [SerializeField] private Vector3 _cameraDistanceOnFinishedFirstRoad;
    [SerializeField] private float _speedUpdatePositionOnUpgrade;
    [SerializeField] private float _speedUpdatePositionOnFinishedFirstRoad;
    [SerializeField] private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    private CinemachineVirtualCamera _camera;
    private CinemachineTransposer _cinemachineTransposer;
    private Player _player;
    private Vector3 _cameraDistanceStartPosition;
    private Coroutine _smoothUpdatePositionJob;
    private Vector3 _targetPosition;

    public void Init(Player player)
    {
        _player = player;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        _player.UpgradingVenom.PlayerWasUpgraded += OnPlayerWasUpgraded;
        _player.SwitchedRoad += OnFinishedFirstRoad;
        _player.ArrivedOnFinish += OnPlayerOnFisnish;
        _cameraDistanceStartPosition = _cinemachineTransposer.m_FollowOffset;
    }

    private void OnPlayerWasUpgraded()
    {
        _targetPosition = _cinemachineTransposer.m_FollowOffset + _cameraDistanceStepOffset;
        if (_smoothUpdatePositionJob != null)
        {
            StopCoroutine(_smoothUpdatePositionJob);
        }
        _smoothUpdatePositionJob = StartCoroutine(SmoothUpdatePosition(_targetPosition, _speedUpdatePositionOnUpgrade));
    }

    public void ResetPosition()
    {
        _cinemachineTransposer.m_FollowOffset = _cameraDistanceStartPosition;
    }

    private IEnumerator SmoothUpdatePosition(Vector3 targetPosition,float speedUpdatePosition)
    {
        while (_cinemachineTransposer.m_FollowOffset != targetPosition)
        {
            _cinemachineTransposer.m_FollowOffset = Vector3.MoveTowards(_cinemachineTransposer.m_FollowOffset, targetPosition, speedUpdatePosition * Time.deltaTime);
            yield return null;
        }
    }

    private void OnFinishedFirstRoad()
    {
        _camera.m_Follow = _player.EnemyContainer.transform;
        _camera.m_LookAt = _player.EnemyContainer.transform;
        _targetPosition = _cameraDistanceOnFinishedFirstRoad;
    }


    private void OnPlayerOnFisnish()
    {
        _camera.m_Follow = _player.transform;
        _camera.m_LookAt = _player.transform;
    }

    private void OnDisable()
    {
        _player.UpgradingVenom.PlayerWasUpgraded -= OnPlayerWasUpgraded;
        _player.SwitchedRoad -= OnFinishedFirstRoad;
        _player.ArrivedOnFinish -= OnPlayerOnFisnish;
    }


}
