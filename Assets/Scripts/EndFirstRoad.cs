using RunnerMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndFirstRoad : MonoBehaviour
{
    public event UnityAction PlayerFinishedFirstRoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerFinishedFirstRoad?.Invoke();
        }
    }
}
