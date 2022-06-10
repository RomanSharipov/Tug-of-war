using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParamsDistance 
{
    [SerializeField] private float _maxDistanceToPlayer = 15;
    [SerializeField] private float _minDistanceToPlayer = 5;
    [SerializeField] private float _maxDistanceToPlayerForFinish = 39f;
    [SerializeField] private float _minDistanceToPlayerForFinish = 30f;
    [SerializeField] private float _speedReduceDistance = 15f;
    [SerializeField] private float _speedAddDistance = 0.3f;
    [SerializeField] private float _stepAddDistanceForUpgradgeVenom = 1.5f;

    public float MaxDistanceToPlayer => _maxDistanceToPlayer;
    public float MinDistanceToPlayer => _minDistanceToPlayer;
    public float MaxDistanceToPlayerForFinish => _maxDistanceToPlayerForFinish;
    public float MinDistanceToPlayerForFinish => _minDistanceToPlayerForFinish;
    public float SpeedReduceDistance => _speedReduceDistance;
    public float SpeedAddDistance => _speedAddDistance;
    public float StepAddDistanceForUpgradgeVenom => _stepAddDistanceForUpgradgeVenom;

    public void AddDistanceForUpgradgeVenom()
    {
        _maxDistanceToPlayer += _stepAddDistanceForUpgradgeVenom;
        _minDistanceToPlayer += _stepAddDistanceForUpgradgeVenom;
    }

    public void SetDistanceToPlayerOnFinish()
    {
        _maxDistanceToPlayer = _maxDistanceToPlayerForFinish;
        _minDistanceToPlayer = _minDistanceToPlayerForFinish;
    }
}
