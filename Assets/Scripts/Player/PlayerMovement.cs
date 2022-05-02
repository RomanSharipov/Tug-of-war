using RunnerMovementSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovementSystem _roadMovement;
    private Player _player;

    public void Init(MovementSystem roadMovement,Player player)
    {
        _roadMovement = roadMovement;
    }

    private void Update()
    {
        _roadMovement.MoveForward();
    }
}
