using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _stepReduceAnimationSpeed = 0.05f;
    
    [SerializeField] private float _minSpeed = 0.5f;
    [SerializeField] private float _maxSpeed = 1.5f;
    [SerializeField] private float _speedAttackAnimation = 0.8f;

    private Animator _animator;
    private Player _player;
    private float _oldSpeedAnimation;

    public float CurrentSpeed => _animator.speed;

    public void Init(Player player)
    {
        _player = player;
        _animator = GetComponent<Animator>();
        _player.Died += OnFall;
        _player.StoppedMoving += OnStop;
        _player.StartedMoving += OnStart;
        _player.Attacked += OnAttack;
        _oldSpeedAnimation = 1f;
    }

    public void ReduceSpeedAnimation(float value)
    {
        SetSpeed(_animator.speed -= value);
    }

    public void AddSpeedAnimation(float value)
    {
        SetSpeed(_animator.speed += value);
    }

    public void SetSpeed(float value)
    {
        if (value > _maxSpeed)
        {
            _animator.speed = _maxSpeed;
            return;
        }

        if (value <= _minSpeed)
        {
            _animator.speed = _minSpeed;
            return;
        }

        else
        {
            _animator.speed = value;
        }
    }

    private void OnFall()
    {
        _animator.speed = 1;
        _animator.SetTrigger(Params.Fall);
    }

    private void OnStop()
    {
        _animator.SetTrigger(Params.Stop);
    }

    private void OnStart()
    {
        _animator.SetTrigger(Params.Start);
    }

    private void OnAttack()
    {
        _oldSpeedAnimation = _animator.speed;
        _animator.speed = _speedAttackAnimation;
        _animator.SetTrigger(Params.Attack);

    }

    private void OnDisable()
    {
        _player.Died -= OnFall;
        _player.StoppedMoving -= OnStop;
        _player.StartedMoving -= OnStart;
        _player.Attacked -= OnAttack;
    }

    public void SetOldSpeedAnimation()
    {
        _animator.speed = _oldSpeedAnimation;
    }
}