using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    [SerializeField] private SegmentBuilding[] _segments;
    [SerializeField] private GameObject _unitedBuilding;
    [SerializeField] private float _delayBeforeDestroySegment = 4;
    [SerializeField] private float _delayBeforeCrushBuilding = 0.5f;

    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        foreach (var segment in _segments)
        {
            segment.Init();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.EnemyAnimator.Fall();
            enemy.TakeOffLasso();
            //CrushBuilding();
            Invoke(nameof(CrushBuilding), _delayBeforeCrushBuilding);
        }
    }



    private void CrushBuilding()
    {
        _boxCollider.enabled = false;
        _unitedBuilding.SetActive(false);
        foreach (var segment in _segments)
        {
            segment.SwithOnRigidbody();

            Destroy(segment.gameObject, _delayBeforeDestroySegment);
        }
        
    }
}
