using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private float _damage = 5;
    [SerializeField] private float _delayBerofeApplyDamage = 0.3f;
    [SerializeField] private int _levelVenomForAttack = 5;

    private Animator _animator;
    public int LevelVenomForAttack => _levelVenomForAttack;
    public float Damage => _damage;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(0, -1, Random.value);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player.UpgradingVenom.CurrentLevelVenom >= _levelVenomForAttack)
            {
                player.Attack();
                InvokeRepeating(nameof(TakeDamage), _delayBerofeApplyDamage, 0);
            }

            else
            {
                _animator.SetTrigger(Params.Attack);
                player.TakeDamage(_damage);
            }

        }
    }

    public void TakeDamage()
    {
        _animator.enabled = false;
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }


    
}
