using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private EndFirstRoad _endFirstRoad;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private SpawnerObjects _spawnerRewards;
    [SerializeField] private SpawnerObjects _spawnerBuildings;
    [SerializeField] private EnemyContainer _enemyContainerTemplate;
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private RoadSegment _firstRoad;
    [SerializeField] private Button _button;
    [SerializeField] private RoadSegment _secondRoad;
    [SerializeField] private SwipeInput _swipeInput;

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
        _player.Init(_firstRoad,_secondRoad);
        _playerCamera.Init(_player);
        //_endRoad.Init(_player.MovementSystem);
        _enemyContainer = Instantiate(_enemyContainerTemplate, _player.EnemyContainerPoint.position, _player.EnemyContainerPoint.rotation);
        
        _enemyContainer.Init(_player, _swipeInput);
        
        _buttonContinue.onClick.AddListener(_enemyContainer.StartFly);
        _buttonContinue.onClick.AddListener(_player.SwitchRoad);

        _buttonContinue.onClick.AddListener(delegate { _swipeInput.gameObject.SetActive(true); });

        
        _spawnerEnemies.Spawn(_player, _enemyContainer, _firstRoad);
        _spawnerRewards.Spawn();
        _spawnerBuildings.Spawn();
        _endFirstRoad.PlayerFinishedFirstRoad += _player.OnFinishedFirstRoad;


    }



    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
