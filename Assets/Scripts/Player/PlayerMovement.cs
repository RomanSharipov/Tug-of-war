using RunnerMovementSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovementSystem _roadMovement;

    public void Init(MovementSystem roadMovement)
    {
        _roadMovement = roadMovement;
    }

    private void Update()
    {
        _roadMovement.MoveForward();
    }
}
