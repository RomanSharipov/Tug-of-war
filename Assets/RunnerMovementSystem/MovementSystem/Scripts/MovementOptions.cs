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

        private Player _player;
        private float _startSpeed;
        private float _lastValueSpeed;

        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float BorderOffset => _borderOffset;

        public event UnityAction<float> SpeedChanged;

        public void Init(Player player)
        {
            _player = player;
            _player.WasTookDamage += ReduceSpeed;
            _player.WasTookHealth += AddSpeed;
            _player.Died += OnDied;
            _startSpeed = MoveSpeed;

        }

        public void ReduceSpeed(float value)
        {
            SetSpeed(_moveSpeed -= GetTotalValue(value));
        }

        public void AddSpeed(float stepAddSpeed)
        {
            SetSpeed(_moveSpeed += GetTotalValue(stepAddSpeed));
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
                
                SpeedChanged?.Invoke(_moveSpeed);
                return;
            }

            if (value <= _minSpeed)
            {
                _moveSpeed = _minSpeed;
                SpeedChanged?.Invoke(_moveSpeed);
                return;
            }

            else
            {
                _moveSpeed = value;
                SpeedChanged?.Invoke(_moveSpeed);
            }
        }

        private float GetTotalValue(float percent)
        {
            return _startSpeed / Params.OneHundredPercent * percent;
        }

        private void OnDied()
        {
            Stop();
        }

        public void OnDisable()
        {
            _player.WasTookDamage -= ReduceSpeed;
            _player.WasTookHealth -= AddSpeed;
        }

    }
}