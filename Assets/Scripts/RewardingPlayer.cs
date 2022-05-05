using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RewardingPlayer : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _rewardAmount = 5;
    [SerializeField] private float _targetRadius = 0.05f;
    [SerializeField] private float _speedReduceRadius = 10f;
    [SerializeField] private ParticleSystem _splashTemplate;

    private Animator _animator;
    private Collider[] _colliders;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(0, -1, Random.value);
    }

    private void RewardPlayer(Player player)
    {
        Instantiate(_splashTemplate,transform.position,transform.rotation);
        player.TakeHealth(_rewardAmount);
        if (player.CurrentHealth % player.UpgradingVenom.RequiredHealthForUpgrade == 0)
        {
            player.UpgradingVenom.UpgradeVenomLevel();
        }

        player.CurrentModelVenom.PlayerAnimator.AddSpeedAnimation(_rewardAmount / Params.OneHundredPercent);
        StartCoroutine(SmoothReduceScale());
        
    }

    private IEnumerator SmoothReduceScale()
    {
        Vector3 targetScale = new Vector3();
        targetScale.Set(_targetRadius, _targetRadius, _targetRadius);
        while (transform.localScale.x > _targetRadius)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, _speedReduceRadius * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            RewardPlayer(player);
        }
    }
}
