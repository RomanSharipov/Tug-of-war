using UnityEngine;

public class RewardingPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _player;
    [SerializeField] private float _frequencyChecking;
    [SerializeField] private float _radius;
    [SerializeField] private int _stepAddSpeed = 1;
    [SerializeField] private float _stepAddAnimationSpeed = 0.1f;
    

    private Collider[] _colliders;

    private void Start()
    {
        InvokeRepeating(nameof(CheckPlayerNearby),0, _frequencyChecking);
        
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
        player.MovementSystem.MovementOptions.AddSpeed(_stepAddSpeed);
        player.PlayerAnimator.SlowUpAnimation(_stepAddAnimationSpeed);
        Destroy(gameObject);
    }
}
