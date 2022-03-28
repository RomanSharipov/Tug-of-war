using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerCamera PlayerCamera;
    [SerializeField] private Player player;
    [SerializeField] private Enemy[] _enemies;

    private void Start()
    {
        player.Init();
        PlayerCamera.Init(player);

        foreach (var enemy in _enemies)
        {
            enemy.Init(player);
        }
    }
}
