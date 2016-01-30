using UnityEngine;
using System.Collections;

public class PigeonAnimatorDriver
{
    private Animator _animator;

    public PigeonAnimatorDriver(Animator animator)
    {
        _animator = animator;
    }

    public float Speed
    {
        get
        {
            return _animator.GetFloat("Speed");
        }

        set
        {
            _animator.SetFloat("Speed", value);
        }
    }
}
