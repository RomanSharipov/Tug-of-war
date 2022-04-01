using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 0.1f;
    [SerializeField] private float _maxSpeed = 1.5f;

    private Animator _animator;
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _animator = GetComponent<Animator>();
        _player.Died += OnFall;
        _player.StoppedMoving += OnStop;
        _player.StartedMoving += OnStart;
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

    private void OnFall()
    {
        _animator.speed = 1;
        _animator.SetTrigger(Params.Fall);
    }

    private void OnStop()
    {
        _animator.speed = 1;
        _animator.SetTrigger(Params.Stop);
    }

    private void OnStart()
    {
        _animator.speed = 1;
        _animator.SetTrigger(Params.Start);
    }

    public class Params
    {
        public const string Fall = "Fall";
        public const string Stop = "Stop";
        public const string Start = "Start";
    }

    private void OnDisable()
    {
        _player.Died -= OnFall;
        _player.StoppedMoving -= OnStop;
        _player.StartedMoving -= OnStart;
    }
}