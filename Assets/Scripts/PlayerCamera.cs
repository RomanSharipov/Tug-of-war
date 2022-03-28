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

    public void Init(Player player)
    {
        _player = player;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineFramingTransposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _player.UpgradingVenom.PlayerWasUpgraded += UpdatePosition;
    }

    private void UpdatePosition()
    {
        _cinemachineFramingTransposer.m_CameraDistance += _cameraDistanceStepOffset;
        _cinemachineFramingTransposer.m_TrackedObjectOffset += _upgradingStepOffset;
    }
}
