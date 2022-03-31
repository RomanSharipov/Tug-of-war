using RunnerMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoad : MonoBehaviour
{
    [SerializeField] private RoadSegment roadSegment;
    [SerializeField] private Button _button;

    private MovementSystem _movementSystem;
    
    public void Init(MovementSystem movementSystem)
    {
        _movementSystem = movementSystem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _button.gameObject.SetActive(true);
            
        }
    }

    public void SwitchRoad()
    {
        _movementSystem.Init(roadSegment);
        _button.gameObject.SetActive(false);
    }
}
