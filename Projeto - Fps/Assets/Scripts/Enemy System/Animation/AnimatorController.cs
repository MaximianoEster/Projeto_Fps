using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator = default;

    public void LocomotionAnimation(float velocity)
    {
        _animator.SetFloat(AnimationParameters.Velocity, velocity);
    }
    
}
