using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private VictoryScreen _victoryScreen;

    private int _currentCountStars;
    private int _maxReward = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Finish();
            if (player.Score == 0)
                player.Lose();

            else
            {
                float result = ((float)player.Score / _game.CountBuildings) * _maxReward;
                _currentCountStars = Mathf.CeilToInt(result);
                player.Win();
                _victoryScreen.gameObject.SetActive(true);
                
                _victoryScreen.ShowStars(_currentCountStars);
            }
        }
    }

}
