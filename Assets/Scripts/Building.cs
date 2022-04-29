using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    [SerializeField] private SegmentBuilding[] _segments;
    [SerializeField] private GameObject _unitedBuilding;
    [SerializeField] private float _delayBeforeDestroySegment = 4;

    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        foreach (var segment in _segments)
        {
            segment.Init();
        }
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
