using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    [SerializeField] private SegmentBuilding[] _segments;
    [SerializeField] private GameObject _unitedBuilding;
    [SerializeField] private float _delayBeforeDestroySegment = 4;
    [SerializeField] private SpawnerButton _spawnerButton;
    [SerializeField] private int _maxCountStickmansOnBuilding;

    public UnityEvent WasCollisionWithEnemyContainer;

    private BoxCollider _boxCollider;
    private int _currentCountStickmansOnBuilding = 0;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (_spawnerButton.IsButtonPressed)
            {
                if (_currentCountStickmansOnBuilding < _maxCountStickmansOnBuilding)
                {
                    enemy.TakeOffLasso();
                    _currentCountStickmansOnBuilding++;
                    return;
                }
                CrushBuilding();

                WasCollisionWithEnemyContainer?.Invoke();
            }
            else
            {
                WasCollisionWithEnemyContainer?.Invoke();
            }
        }

    }
}
