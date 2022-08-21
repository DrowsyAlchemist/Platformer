using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string RunAnimation = "IsRunning";
    private const string JumpAnimation = "IsJumping";
    private const string DieAnimation = "IsDead";
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        ResetAnimations();
    }

    public void PlayRun()
    {
        ResetAnimations();
        _animator.SetBool(RunAnimation, true);
    }

    public void PlayJump()
    {
        ResetAnimations();
        _animator.SetBool(JumpAnimation, true);
    }

    public void PlayDie()
    {
        ResetAnimations();
        _animator.SetBool(DieAnimation, true);
    }

    private void ResetAnimations()
    {
        _animator.SetBool(RunAnimation, false);
        _animator.SetBool(JumpAnimation, false);
        _animator.SetBool(DieAnimation, false);
    }
}
