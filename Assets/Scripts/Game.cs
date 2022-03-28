using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerCamera PlayerCamera;
    [SerializeField] private Player player;

    private void Start()
    {
        player.Init();
        PlayerCamera.Init(player);
    }
}
