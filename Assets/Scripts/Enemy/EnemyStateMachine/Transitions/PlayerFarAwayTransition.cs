using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarAwayTransition : Transition
{
    [SerializeField] private float _maxDistance;

    private float _currentDistance;

    public override bool NeedTransit { get ; set ; }

    private void Update()
    {
        _currentDistance = Vector3.Distance(transform.position,Enemy.Player.transform.position);

        if (_currentDistance > _maxDistance)
            NeedTransit = true;
    }
}
