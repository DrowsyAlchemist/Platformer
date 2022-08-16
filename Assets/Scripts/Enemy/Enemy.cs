using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Patrolling))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _recoveryTime = 2;
    [SerializeField] private UnityEvent _isWin;

    protected EnemyAnimator EnemyAnimator;
    private const float DestroyDelay = 1f;
    private const float RestartDelay = 2f;
    private const float WinDelay = 0.7f;
    private Vector3 _initialPosition;
    private Patrolling _patrolling;

    public event UnityAction IsWin
    {
        add => _isWin.AddListener(value);
        remove => _isWin.RemoveListener(value);
    }

    public bool IsDizzy { get; private set; }

    private void OnEnable()
    {
        EnemyAnimator = GetComponent<EnemyAnimator>();
        _patrolling = GetComponent<Patrolling>();
    }

    private void Start()
    {
        if (_patrolling.CanPatrol)
            _patrolling.StartPatrol();

        _initialPosition = transform.position;
    }

    public void OnEnemiesWin()
    {
        _patrolling.StopPatrol();
        EnemyAnimator.PlayWin();
        StartCoroutine(Restart());
    }

    public void OnPlayerWin()
    {
        _patrolling.StopPatrol();
        StartCoroutine(DieAndDestroy());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(RestartDelay);
        transform.position = _initialPosition;
        EnemyAnimator.PlayIdle();

        if (_patrolling.CanPatrol)
            _patrolling.StartPatrol();
    }

    private IEnumerator DieAndDestroy()
    {
        EnemyAnimator.PlayDie();
        yield return new WaitForSeconds(DestroyDelay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        ReactToPlayerCollision(collision.collider, AttackPlayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReactToPlayerCollision(collision, StartDizzy);
    }

    private void ReactToPlayerCollision(Collider2D playerCollider, UnityAction action)
    {
        if (playerCollider.gameObject.TryGetComponent(out Player _))
            if (IsDizzy == false)
                action();
    }

    private void AttackPlayer()
    {
        Debug.Log("AttackPlayer");
        _patrolling.StopPatrol();
        EnemyAnimator.PlayAttack();
        StartCoroutine(InvokeWinEvent());
    }

    private IEnumerator InvokeWinEvent()
    {
        yield return new WaitForSeconds(WinDelay);
        _isWin.Invoke();
    }

    private void StartDizzy()
    {
        _patrolling.StopPatrol();
        IsDizzy = true;
        StartCoroutine(Recover());
    }

    private IEnumerator Recover()
    {
        EnemyAnimator.PlayDizzy();
        Debug.Log("Recovering...");
        var waitForSeconds = new WaitForSeconds(_recoveryTime);
        yield return waitForSeconds;
        IsDizzy = false;
        Debug.Log("Recovered!");

        if (_patrolling.CanPatrol)
            _patrolling.StartPatrol();
    }
}
