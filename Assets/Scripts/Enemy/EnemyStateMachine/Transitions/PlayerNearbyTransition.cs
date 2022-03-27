using UnityEngine;

public class PlayerNearbyTransition : Transition
{
    [SerializeField] private float _distanceToPlayer;

    private bool _playerNearby;

    public override bool NeedTransit { get; set; }

    private void Update()
    {
        _playerNearby = Vector3.Distance(Enemy.Transform.position, Enemy.Player.Transform.position) < _distanceToPlayer;

        if (_playerNearby)
        {
            NeedTransit = true;
        }
    }
}
