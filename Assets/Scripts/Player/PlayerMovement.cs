using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _stepReduceSpeed;

    private Player _player;
    private Vector3 _direction;
    private Transform _transform;
    private Quaternion _rotation;
    private bool _isWalking;

    public bool IsWalking => _isWalking;
    public float Speed => _speed;

    public void Init()
    {
        _transform = GetComponent<Transform>();
        _player = GetComponent<Player>();
        _player.PlayerInput.Walked += Move;
        
    }

    private void Move(Vector2 direction)
    {
        _isWalking = true;
        _direction.Set(direction.x, 0, direction.y);
        _direction.Normalize();
        _transform.Translate(_direction * Time.deltaTime * _speed, Space.World);

        if (_direction != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(_direction, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _rotation, _rotationSpeed);
        }
    }

    private void OnDisable()
    {
        _player.PlayerInput.Walked -= Move;
        
    }

    public void ReduceSpeed()
    {
        if (_speed > _minSpeed)
        {
            _speed -= _stepReduceSpeed;
        }
        
    }
}