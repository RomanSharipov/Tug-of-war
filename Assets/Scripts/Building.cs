using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SegmentBuilding[] _segments;
    [SerializeField] private GameObject _unitedBuilding;
    [SerializeField] private float _delayBeforeDestroy = 3;

    private void Start()
    {
        foreach (var segment in _segments)
        {
            segment.Init();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyContainer enemyContainer))
        {
            foreach (var segment in _segments)
            {
                segment.SwithOnRigidbody();
                Destroy(segment, _delayBeforeDestroy);
            }
            _unitedBuilding.SetActive(false);
        }
    }
}
