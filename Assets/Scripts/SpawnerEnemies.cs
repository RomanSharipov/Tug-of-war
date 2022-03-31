using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;

public class SpawnerEnemies : MonoBehaviour
{
    private Transform[] _points;
    

    [SerializeField] private List<Enemy> _allEnemies = new List<Enemy>();

    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private Player _player;

    public void Spawn(Player player,EnemyContainer enemyContainer,RoadSegment roadSegment)
    {
        _player = player;
        
        _points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i);
        }

        foreach (var point in _points)
        {
            Enemy newEnemy = Instantiate(_enemyTemplate, point.position, point.rotation,transform);
            newEnemy.Init(_player, enemyContainer, roadSegment);
            _allEnemies.Add(newEnemy);
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (var enemy in _allEnemies)
        {
            Destroy(enemy.gameObject);
            
        }
        _allEnemies = new List<Enemy>();
    }
}
