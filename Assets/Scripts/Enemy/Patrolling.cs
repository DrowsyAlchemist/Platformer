using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Patrolling : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed = 2;
    [SerializeField] private float _haltDuration = 2;
    [SerializeField] private Transform[] _patrolPoints;

    private const float DeltaPointDistance = 0.1f;
    private EnemyAnimator _enemyAnimator;
    private bool _isPatrolInProgress;
    private bool _needStopPatrol;

    public bool CanPatrol => _patrolPoints.Length > 0;
    public bool IsPatrol => _isPatrolInProgress;

    private void OnEnable()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    public bool TryStartPatrol()
    {
        bool canStartPatrol = CanPatrol && (_isPatrolInProgress == false);

        if (canStartPatrol)
        {
            _needStopPatrol = false;
            StartCoroutine(Patrol());
        }
        return canStartPatrol;
    }

    public void StopPatrol()
    {
        _needStopPatrol = true;
    }

    private IEnumerator Patrol()
    {
        _isPatrolInProgress = true;
        var waitForSeconds = new WaitForSeconds(_haltDuration);

        while (gameObject.activeSelf)
        {
            foreach (var point in _patrolPoints)
            {
                while (IsPointReached(point) == false)
                {
                    if (_needStopPatrol)
                    {
                        _isPatrolInProgress = false;
                        yield break;
                    }
                    TakeStepToPoint(point);
                    yield return null;
                }
                if (_needStopPatrol)
                {
                    _isPatrolInProgress = false;
                    yield break;
                }
                _enemyAnimator.PlayIdle();
                yield return waitForSeconds;
            }
        }
    }

    private bool IsPointReached(Transform point)
    {
        return Mathf.Abs(transform.position.x - point.position.x) < DeltaPointDistance;
    }

    private void TakeStepToPoint(Transform point)
    {
        _enemyAnimator.PlayWalk();
        transform.MoveAlongXAxis(point.transform, _walkingSpeed * Time.deltaTime);
    }

    private void OnValidate()
    {
        if (_walkingSpeed < 0)
            _walkingSpeed *= -1;
    }
}
