using UnityEngine;

public class Venom : MonoBehaviour
{
    [SerializeField] private Transform  _lassoJointPoint;

    private Animator _animator;
    private PlayerAnimator _playerAnimator;

    public Transform LassoJointPoint => _lassoJointPoint;
    
    public PlayerAnimator PlayerAnimator => _playerAnimator;

    public void Init(Player player)
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init(player);
    }
}
