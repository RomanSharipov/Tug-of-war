using UnityEngine;

public class Venom : MonoBehaviour
{
    [SerializeField] private Transform  _lassoJointPoint;
    [SerializeField] private GameObject _totalJointLassoPointTemplate;
    [SerializeField] private float _widthAreaForLassoPoint = 1f;

    private Animator _animator;
    private PlayerAnimator _playerAnimator;

    public Transform LassoJointPoint => _lassoJointPoint;
    
    public PlayerAnimator PlayerAnimator => _playerAnimator;

    public void Init(Player player)
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init(player);
    }

    public GameObject GetEndPointLasso()
    {
        var endPointLasso = Instantiate(_totalJointLassoPointTemplate, _lassoJointPoint);
        endPointLasso.transform.position = Random.insideUnitSphere * _widthAreaForLassoPoint + LassoJointPoint.position;
        return endPointLasso;
    } 
    //var totalLassoJointPoint = Instantiate(_totalJointLassoPointTemplate, _player.transform);
    //totalLassoJointPoint.transform.position = Random.insideUnitSphere* _widthAreaForLassoPoint + Player.CurrentModelVenom.LassoJointPoint.position;
}
