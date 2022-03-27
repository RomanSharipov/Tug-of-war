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
        [SerializeField] private float _stepReduceSpeed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float BorderOffset => _borderOffset;

        public void ReduceSpeed()
        {
            if (_moveSpeed > _minSpeed)
            {
                _moveSpeed -= _stepReduceSpeed;
            }

        }

        public void AddSpeed(int stepAddSpeed)
        {
            if (_moveSpeed < _maxSpeed)
            {
                _moveSpeed += stepAddSpeed;
            }

        }

    }
}