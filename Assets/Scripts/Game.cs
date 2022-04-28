using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private EndFirstRoad _endFirstRoad;
    [SerializeField] private Transform _playerSpawnPoint;
    
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private EnemyContainer _enemyContainerTemplate;
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private RoadSegment _firstRoad;
    
    [SerializeField] private RoadSegment _secondRoad;
    

    [SerializeField] private Player _player;
    private EnemyContainer _enemyContainer;

    private void Start()
    {
        StartGame();
        
    }

    public void StartGame()
    {
        _endFirstRoad.PlayerFinishedFirstRoad += _player.OnFinishedFirstRoad;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
