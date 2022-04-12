using RunnerMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndFirstRoad : MonoBehaviour
{
    public event UnityAction PlayerFinishedFirstRoad;
    [SerializeField] private SwitcherButton _switcherButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerFinishedFirstRoad?.Invoke();
            _switcherButton.SwitchOnButton();
        }
    }
}
