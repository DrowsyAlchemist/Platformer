using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Patrolling))]
public class Enemy : MonoBehaviour, IRestartable
{
    [SerializeField] private float _recoveryTime = 2;

    protected EnemyAnimator EnemyAnimator;
    private const float DestroyDelay = 1f;
    private GameFinishTrigger _gameFinishTrigger;
    private Vector3 _initialPosition;
    private Patrolling _patrolling;
    private bool _isWin;

    public bool IsDizzy { get; private set; }

    private void OnEnable()
    {
        EnemyAnimator = GetComponent<EnemyAnimator>();
        _patrolling = GetComponent<Patrolling>();
        _initialPosition = transform.position;

        _gameFinishTrigger = FindObjectOfType<GameFinishTrigger>();
        _gameFinishTrigger.PlayerWin += OnPlayerWin;
        _gameFinishTrigger.PlayerLost += OnEnemiesWin;

        _patrolling.TryStartPatrol();
    }

    public void Restart()
    {
        _isWin = false;
        transform.position = _initialPosition;
        EnemyAnimator.PlayIdle();
        _patrolling.TryStartPatrol();
    }

    private void OnEnemiesWin()
    {
        _patrolling.StopPatrol();
        EnemyAnimator.PlayWin();
    }

    private void OnPlayerWin()
    {
        _patrolling.StopPatrol();
        StartCoroutine(DieAndDestroy());
    }

    private IEnumerator DieAndDestroy()
    {
        EnemyAnimator.PlayDie();
        yield return new WaitForSeconds(DestroyDelay);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _gameFinishTrigger.PlayerWin -= OnPlayerWin;
        _gameFinishTrigger.PlayerLost -= OnEnemiesWin;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReactToPlayerCollision(collision.collider, AttackPlayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReactToPlayerCollision(collision, StartDizzy);
    }

    private void ReactToPlayerCollision(Collider2D playerCollider, UnityAction action)
    {
        if (playerCollider.gameObject.TryGetComponent(out Player _))
            if (_isWin == false && IsDizzy == false)
                action();
    }

    private void AttackPlayer()
    {
        _patrolling.StopPatrol();
        EnemyAnimator.PlayAttack();
        _isWin = true;
    }

    private void StartDizzy()
    {
        _patrolling.StopPatrol();
        IsDizzy = true;
        EnemyAnimator.PlayDizzy();
        StartCoroutine(Recover());
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(_recoveryTime);
        IsDizzy = false;
        EnemyAnimator.PlayIdle();
        _patrolling.TryStartPatrol();
    }
}