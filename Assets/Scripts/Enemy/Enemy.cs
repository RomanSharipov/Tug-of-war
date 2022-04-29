using System.Collections;
using UnityEngine;
using RunnerMovementSystem;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(MovementSystem))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _radiusSphereOverlast = 1f;
    [SerializeField] private CableProceduralCurve _cableProceduralCurve;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private RoadSegment _roadSegment;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _speedFlowDown;
    [SerializeField] private ParticleSystem _airTrail;
    [SerializeField] private float _minHeight = 0;
    [SerializeField] private float _maxHeight = 3;
    [SerializeField] private float _speedSettingRandomHeight = 5;
    [SerializeField] private Vector3 _targetPosition = new Vector3();
    


    private EnemyMovement _enemyMovement;
    private MovementSystem _movementOnWay;
    private Transform _transform;
    private EnemyStateMachine _enemyStateMachine;
    private EnemyAnimator _enemyAnimator;
    
    private Vector3 _direction;
    private Quaternion _targetRotation;
    private Collider[] _colliders;
    private CapsuleCollider _capsuleCollider;

    public Player Player => _player;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public Transform Transform => _transform;
    public EnemyAnimator EnemyAnimator => _enemyAnimator;
    public MovementSystem MovementOnWay => _movementOnWay;
    public EnemyContainer EnemyContainer => _enemyContainer;

    public void Init()
    {
        _transform = GetComponent<Transform>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _enemyMovement = new EnemyMovement();
        _enemyMovement.Init(_transform);
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyStateMachine.Init();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAnimator.Init();
        _movementOnWay = GetComponent<MovementSystem>();
        _movementOnWay.Init(_roadSegment);
        _player.ModelWasChanged += SwitchEndPointLasso;
    }

    public void ThrowLassoOnPlayer()
    {
        EnemyContainer.AddEnemy(this);
        transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
        _cableProceduralCurve.SetEndPoint(Player.CurrentModelVenom.GetEndPointLasso().transform);
        _cableProceduralCurve.gameObject.SetActive(true);
        Player.TakeDamage(_damage);
        EnemyAnimator.PullRope();
        SwitchOffMovement();
        
    }

    public void SwitchEndPointLasso()
    {
        _cableProceduralCurve.SetEndPoint(Player.CurrentModelVenom.GetEndPointLasso().transform);
    }

    private void OnDisable()
    {
        _player.ModelWasChanged -= SwitchEndPointLasso;
    }

    public void SwitchOffMovement()
    {
        Debug.Log("SwitchOffMovement");
        _enemyStateMachine.Current.enabled = false;
        _enemyStateMachine.enabled = false;
        _movementOnWay.enabled = false;
    }

    public void TakeOffLasso()
    {
        _cableProceduralCurve.gameObject.SetActive(false);
        transform.parent = null;
        _capsuleCollider.enabled = false;
        StartCoroutine(FlowDown());
        _enemyContainer.ThrowOutStickman(this);
        EnemyAnimator.Fall();
        _airTrail.gameObject.SetActive(false);
    }


    private IEnumerator FlowDown()
    {
        while (IsGroundNearby() == false)
        {
            transform.Translate(-Vector3.up * _speedFlowDown * Time.deltaTime);
            yield return null;
        }
    }

    public bool IsGroundNearby()
    {
        _colliders = Physics.OverlapSphere(transform.position, _radiusSphereOverlast, _ground);
        
        return _colliders.Length > 0;
    }

    public void StartFly()
    {
        EnemyAnimator.HangRope();
        _airTrail.gameObject.SetActive(true);
        SetRandomHeight();
    }

    private void SetRandomHeight()
    {
        StartCoroutine(SmoothSetRandomHeight());
    }

    private IEnumerator SmoothSetRandomHeight()
    {
        _targetPosition.Set(transform.position.x, Random.Range(_minHeight,_maxHeight), transform.position.z);

        while (transform.position.y < _targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speedSettingRandomHeight * Time.deltaTime);
            yield return null;
        }
    }
}
