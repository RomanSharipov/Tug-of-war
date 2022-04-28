using System;
using UnityEngine;
using UnityEngine.Events;

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

        public event UnityAction<float> SpeedChanged;

        public void ReduceSpeed(float value)
        {
            SetSpeed(_moveSpeed -= value);
            
        }

        public void AddSpeed(float stepAddSpeed)
        {
            SetSpeed(_moveSpeed += stepAddSpeed);
        }

        public void Stop()
        {
            _lastValueSpeed = _moveSpeed;
            _moveSpeed = 0;
        }

        public void SetSpeed(float value)
        {
            if (value > _maxSpeed)
            {
                _moveSpeed = _maxSpeed;
                SpeedChanged(_moveSpeed);
                return;
            }

            if (value <= _minSpeed)
            {
                _moveSpeed = _minSpeed;
                SpeedChanged(_moveSpeed);
                return;
            }

            else
            {
                _moveSpeed = value;
                SpeedChanged(_moveSpeed);
            }
        }

    }
}