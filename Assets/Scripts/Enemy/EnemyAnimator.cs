using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    public void Init()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetBool(Params.Run, true);
    }

    public void PullRope()
    {
        _animator.SetBool(Params.PullRope, true);
    }

    public class Params
    {
        public const string Run = "Run";
        public const string PullRope = "PullRope";
    }
}
