using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string RunAnimation = "IsRunning";
    private const string JumpAnimation = "IsJumping";
    private const string DieAnimation = "Die";
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartRunAnimation()
    {
        _animator.SetBool(RunAnimation, true);
    }

    public void StopRunAnimation()
    {
        _animator.SetBool(RunAnimation, false);
    }

    public void StartJumpAnimation()
    {
        _animator.SetBool(JumpAnimation, true);
    }
    public void StopJumpAnimation()
    {
        _animator.SetBool(JumpAnimation, false);
    }

    public void PlayDieAnimation()
    {
        _animator.SetTrigger(DieAnimation);
    }
}
