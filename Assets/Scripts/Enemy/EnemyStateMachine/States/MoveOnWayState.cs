using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWayState : State
{
    private void Update()
    {
        Enemy.MovementOnWay.MoveForward();
    }
}
