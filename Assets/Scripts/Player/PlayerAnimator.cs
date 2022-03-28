using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 0.1f;
    [SerializeField] private float _maxSpeed = 1.5f;

    private const string Walk = "Walk";
    private Animator _animator;
    private Player _player;
    private Vector3 _startScale;


    public void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    public void SlowDownAnimation(float value)
    {
        if (_animator.speed > _minSpeed) 
        {
            _animator.speed -= value;
        }
    }

    public void SlowUpAnimation(float value)
    {
        if (_animator.speed < _maxSpeed)
        {
            _animator.speed += value;
        }
    }
}