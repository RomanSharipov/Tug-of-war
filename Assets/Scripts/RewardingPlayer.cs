using UnityEngine;
using UnityEngine.Events;

public class RewardingPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _player;
    [SerializeField] private float _frequencyChecking;
    [SerializeField] private float _radius;
    [SerializeField] private int _rewardAmount = 5;
    [SerializeField] private float _stepAddAnimationSpeed = 0.05f;

    private Animator _animator;
    private Collider[] _colliders;

    private void Start()
    {
        InvokeRepeating(nameof(CheckPlayerNearby),0, _frequencyChecking);
        _animator = GetComponent<Animator>();
        _animator.Play(0, -1, Random.value);
    }

    public void CheckPlayerNearby()
    {
        _colliders = Physics.OverlapSphere(transform.position, _radius, _player);
        if (_colliders.Length == 0)
            return;

        if (_colliders[0].TryGetComponent(out Player player))
            RewardPlayer(player);
    }

    private void RewardPlayer(Player player)
    {
        player.TakeHealth(_rewardAmount);
        if (player.CurrentHealth % player.UpgradingVenom.RequiredHealthForUpgrade == 0)
        {
            player.UpgradingVenom.UpgradeVenomLevel();
        }
        
        player.CurrentModelVenom.PlayerAnimator.AddSpeedAnimation(_stepAddAnimationSpeed);
        Destroy(gameObject);
    }
}
