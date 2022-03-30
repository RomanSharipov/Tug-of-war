using RunnerMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoad : MonoBehaviour
{
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private RoadSegment roadSegment;
    [SerializeField] private Button _button;
    [SerializeField] private EnemyContainer _enemyContainer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _button.gameObject.SetActive(true);
            
        }
    }

    public void SwitchRoad()
    {
        movementSystem.Init(roadSegment);
        _button.gameObject.SetActive(false);
    }
}
