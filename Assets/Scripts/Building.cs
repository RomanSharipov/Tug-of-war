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
        if (other.TryGetComponent(out EnemyContainer enemyContainer))
        {
            //enemyContainer.StopMoveForSeconds(1.5f);
            
            //Invoke(nameof(CrushBuilding), _delayBeforeCrushBuilding);
        }


        //if (other.TryGetComponent(out Enemy enemy))
        //{
        //    enemy.TakeOffLasso();

        //    Invoke(nameof(CrushBuilding), _delayBeforeCrushBuilding);
        //}
    }

    public void CrushBuilding()
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
