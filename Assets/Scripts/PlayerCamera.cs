using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraDistanceStepOffset;
    [SerializeField] private Vector3 _cameraDistanceStepOffsetOnFinishedFirstRoad;
    [SerializeField] private float _speedUpdatePositionOnUpgrade;
    [SerializeField] private float _speedUpdatePositionOnFinishedFirstRoad;
    //[SerializeField] private Vector3 _upgradingStepOffset;

    private CinemachineVirtualCamera _camera;
    private CinemachineTransposer _cinemachineTransposer;
    private Player _player;

    private Vector3 _trackedObjectOffsetStartPosition = new Vector3();
    private Vector3 _cameraDistanceStartPosition ;
    private Coroutine _smoothUpdatePositionJob;
    private Vector3 _targetPosition;

    public void Init(Player player)
    {
        _player = player;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        _player.UpgradingVenom.PlayerWasUpgraded += OnPlayerWasUpgraded;
        _player.FinishedFirstRoad += OnFinishedFirstRoad;
        _camera.Follow = player.Transform;
        _camera.LookAt = player.Transform;

        _cameraDistanceStartPosition = _cinemachineTransposer.m_FollowOffset;
        //_trackedObjectOffsetStartPosition = _cinemachineTransposer.m_TrackedObjectOffset;
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
        //_cinemachineTransposer.m_TrackedObjectOffset = _trackedObjectOffsetStartPosition;
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
        _targetPosition = _cameraDistanceStepOffsetOnFinishedFirstRoad;
        if (_smoothUpdatePositionJob != null)
        {
            StopCoroutine(_smoothUpdatePositionJob);
        }
        _smoothUpdatePositionJob = StartCoroutine(SmoothUpdatePosition(_targetPosition, _speedUpdatePositionOnFinishedFirstRoad));
    }
}
