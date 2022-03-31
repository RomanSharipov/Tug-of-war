using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _cameraDistanceStepOffset;
    [SerializeField] private Vector3 _upgradingStepOffset;

    private CinemachineVirtualCamera _camera;
    private CinemachineFramingTransposer _cinemachineFramingTransposer;
    private Player _player;

    private Vector3 _trackedObjectOffsetStartPosition = new Vector3();
    private float _cameraDistanceStartPosition ;

    public void Init(Player player)
    {
        _player = player;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineFramingTransposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _player.UpgradingVenom.PlayerWasUpgraded += UpdatePosition;
        _camera.Follow = player.Transform;
        _camera.LookAt = player.Transform;

        _cameraDistanceStartPosition = _cinemachineFramingTransposer.m_CameraDistance;
        _trackedObjectOffsetStartPosition = _cinemachineFramingTransposer.m_TrackedObjectOffset;
    }

    private void UpdatePosition()
    {
        _cinemachineFramingTransposer.m_CameraDistance += _cameraDistanceStepOffset;
        _cinemachineFramingTransposer.m_TrackedObjectOffset += _upgradingStepOffset;
    }

    public void ResetPosition()
    {
        _cinemachineFramingTransposer.m_CameraDistance = _cameraDistanceStartPosition;
        _cinemachineFramingTransposer.m_TrackedObjectOffset = _trackedObjectOffsetStartPosition;
    }
}
