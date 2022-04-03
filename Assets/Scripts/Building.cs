using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SegmentBuilding[] _segments;
    [SerializeField] private GameObject _unitedBuilding;
    [SerializeField] private float _delayBeforeDestroySegment = 4;
    [SerializeField] private float _delayBeforeCrushBuilding = 0.5f;

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

            Invoke(nameof(CrushBuilding), _delayBeforeCrushBuilding);
        }
    }

    private void CrushBuilding()
    {
        foreach (var segment in _segments)
        {
            segment.SwithOnRigidbody();

            Destroy(segment.gameObject, _delayBeforeDestroySegment);
        }
        _unitedBuilding.SetActive(false);
    }
}
