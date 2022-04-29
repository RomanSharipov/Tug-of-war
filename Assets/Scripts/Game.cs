using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private EndFirstRoad _endFirstRoad;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy[] _enemies;

    private void Start()
    {
        foreach (var _enemy in _enemies)
        {
            _enemy.Init();
        }
        _player.Init();

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
