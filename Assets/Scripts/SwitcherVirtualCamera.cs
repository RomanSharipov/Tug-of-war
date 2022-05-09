using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitcherVirtualCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _enemyContainerCamera;
    [SerializeField] private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _player.ArrivedOnFinish += OnArrivedOnFinish;
        _player.SwitchedRoad += OnSwitchedRoad;
    }

    private void SwitchVirtualCamera(CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        foreach (var camera in cameras)
        {
            camera.Priority = 0;
        }
        cinemachineVirtualCamera.Priority = 7;

    }


    private void OnArrivedOnFinish()
    {
        SwitchVirtualCamera(_playerCamera);
    }

    private void OnSwitchedRoad()
    {
        SwitchVirtualCamera(_enemyContainerCamera);
    }


    private void OnDisable()
    {
        _player.SwitchedRoad -= OnArrivedOnFinish;
        _player.ArrivedOnFinish -= OnSwitchedRoad;
    }
}
