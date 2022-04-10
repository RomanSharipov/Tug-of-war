using RunnerMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoad : MonoBehaviour
{
    
    [SerializeField] private SwitcherButton _switcherButton;

    private MovementSystem _movementSystem;
    
    public void Init(MovementSystem movementSystem)
    {
        _movementSystem = movementSystem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.StopMove();

            _switcherButton.SwitchOnButton();
        }
    }
}
