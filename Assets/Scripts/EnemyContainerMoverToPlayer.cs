using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainerMoverToPlayer : MonoBehaviour
{
    [SerializeField] private float _currentDistance;
    [SerializeField] private float _maxDistanceToPlayer;
    [SerializeField] private float _speedReduceDistance;

    private Player _player;
    private float _speed;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        _currentDistance = Vector3.Distance(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z), transform.position);

        //if (_currentDistance < _minDistanceToPlayer)
        //{
        //    return;
        //}

        MoveToPlayer();

        if (_currentDistance > _maxDistanceToPlayer)
        {
            ReduceDistanceToPlayer();
        }
    }


    private void MoveToPlayer()
    {
        _speed = _player.MovementSystem.CurrentSpeed;
        transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void ReduceDistanceToPlayer()
    {
        transform.position += transform.forward * _speedReduceDistance * Time.deltaTime;
    }
}
