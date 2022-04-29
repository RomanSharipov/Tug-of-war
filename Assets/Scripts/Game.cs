using UnityEngine;
using RunnerMovementSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private EndFirstRoad _endFirstRoad;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Player _player;

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
