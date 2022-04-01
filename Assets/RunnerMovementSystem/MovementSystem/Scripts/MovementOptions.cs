using System;
using UnityEngine;

namespace RunnerMovementSystem
{
    [Serializable]
    public class MovementOptions
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _borderOffset;
        
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private float _lastValueSpeed;

        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float BorderOffset => _borderOffset;

        public void ReduceSpeed(float value)
        {
            if (_moveSpeed > _minSpeed)
            {
                _moveSpeed -= value;
            }

        }

        public void AddSpeed(float stepAddSpeed)
        {
            if (_moveSpeed < _maxSpeed)
            {
                _moveSpeed += stepAddSpeed;
            }
        }

        public void Stop()
        {
            _lastValueSpeed = _moveSpeed;
            _moveSpeed = 0;
        }

        public void StartMove()
        {
            _moveSpeed = _lastValueSpeed;
        }

    }
}