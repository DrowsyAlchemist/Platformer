using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string WalkAnimation = "IsWalking";
    private const string DizzyAnimation = "IsDizzy";
    private const string AttackAnimation = "Attack";
    private const string DieAnimation = "Die";
    private const string WinAnimation = "IsWin";

    private Animator _animator;

    public float CurrentClipDuration => _animator.GetCurrentAnimatorStateInfo(0).length / _animator.GetCurrentAnimatorStateInfo(0).speed;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        ResetAnimations();
    }

    public void PlayWalk()
    {
        ResetAnimations();
        _animator.SetBool(WalkAnimation, true);
    }

    public void PlayDizzy()
    {
        ResetAnimations();
        _animator.SetBool(DizzyAnimation, true);
    }

    public void PlayAttack()
    {
        ResetAnimations();
        _animator.SetTrigger(AttackAnimation);
    }

    public void PlayWin()
    {
        ResetAnimations();
        _animator.SetBool(WinAnimation, true);
    }

    public void PlayDie()
    {
        ResetAnimations();
        _animator.SetTrigger(DieAnimation);
    }

    private void ResetAnimations()
    {
        _animator.SetBool(WalkAnimation, false);
        _animator.SetBool(DizzyAnimation, false);
        _animator.SetBool(WinAnimation, false);












    }
}
