using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private EndRoad _endRoad;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private SpawnerObjects _spawnerRewards;
    [SerializeField] private SpawnerObjects _spawnerBuildings;
    [SerializeField] private EnemyContainer _enemyContainerTemplate;
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private RoadSegment _roadSegment;
    [SerializeField] private Button _button;

    private Player _player;
    private EnemyContainer _enemyContainer;

    private void Start()
    {
        _spawnerBuildings.Init();
        _spawnerRewards.Init();
        StartGame();
    }

    public void StartGame()
    {
        _player = Instantiate(_playerTemplate, _playerSpawnPoint.position, _playerSpawnPoint.rotation);
        _player.Init(_roadSegment);
        _playerCamera.Init(_player);
        _endRoad.Init(_player.MovementSystem);
        _enemyContainer = Instantiate(_enemyContainerTemplate, _player.EnemyContainerPoint.position, _player.EnemyContainerPoint.rotation);
        _enemyContainer.Init(_player);
        _spawnerEnemies.Spawn(_player, _enemyContainer, _roadSegment);
        _spawnerRewards.Spawn();
        _spawnerBuildings.Spawn();
        _button.onClick.AddListener(() => { _enemyContainer.StartFly(); });
    }

    public void DestroyAllObjects()
    {
        Destroy(_player.gameObject);
        Destroy(_enemyContainer.gameObject);
        _spawnerEnemies.DestroyAllEnemies();
        _spawnerRewards.DestroyAllObjects();
    }

    public void RestartGame()
    {
        DestroyAllObjects();
        Invoke(nameof( StartGame),0.1f);
        
        _playerCamera.ResetPosition();
    }


}
