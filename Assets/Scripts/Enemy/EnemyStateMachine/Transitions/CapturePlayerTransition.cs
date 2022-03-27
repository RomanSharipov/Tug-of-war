using UnityEngine;

public class CapturePlayerTransition : Transition
{
    [SerializeField] private float _distanceToThrowLassoPoint;

    private bool _throwLassoPointNearby;

    public override bool NeedTransit { get; set; }

    private void Update()
    {
        _throwLassoPointNearby = Vector3.Distance(Enemy.Transform.position, Enemy.Player.ThrowLassoPoint.position) < _distanceToThrowLassoPoint;

        if (_throwLassoPointNearby)
        {
            NeedTransit = true;
        }
    }
}
