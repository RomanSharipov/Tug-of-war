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
    [SerializeField] private float _explosionPower;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private Transform _explosionPoint;

    public UnityEvent WasCollisionWithEnemyContainer;
    public UnityEvent LimitWasExhaustedMaxCountStickmans;

    private BoxCollider _boxCollider;
    private int _currentCountStickmansOnBuilding = 0;

    public event UnityAction BuilingCrashed;

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
        BuilingCrashed?.Invoke();
        _boxCollider.enabled = false;
        _unitedBuilding.SetActive(false);
        foreach (var segment in _segments)
        {
            segment.SwithOnRigidbody();
            segment.Rigidbody.AddExplosionForce(_explosionPower, _explosionPoint.position, _explosionRadius);
            Destroy(segment.gameObject, _delayBeforeDestroySegment);
        }
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            WasCollisionWithEnemyContainer?.Invoke();
            if (_currentCountStickmansOnBuilding < _maxCountStickmansOnBuilding)
            {
                enemy.TakeOffLasso();
                _currentCountStickmansOnBuilding++;
                return;
            }
            LimitWasExhaustedMaxCountStickmans?.Invoke();

            if (_spawnerButton.IsButtonPressed)
                CrushBuilding();

        }

    }
}
