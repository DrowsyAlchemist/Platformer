using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Patrolling : MonoBehaviour
{
    [SerializeField] Transform[] _patrolPoints;
    [SerializeField] float _walkingSpeed = 2;
    [SerializeField] float _haltDuration = 2;

    private const float DeltaPointDistance = 0.1f;
    private EnemyAnimator _enemyAnimator;

    public bool CanPatrol => _patrolPoints.Length > 0;
    public bool IsPatrol { get; private set; }

    private void OnEnable()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    public void StartPatrol()
    {
        if (CanPatrol)
            StartCoroutine(Patrol());
    }

    public void StopPatrol()
    {
        IsPatrol = false;
    }

    private IEnumerator Patrol()
    {
        IsPatrol = true;
        var waitForSeconds = new WaitForSeconds(_haltDuration);

        while (gameObject.activeSelf)
        {
            foreach (var point in _patrolPoints)
            {
                bool negativeDirection = transform.position.x - point.position.x > 0;

                while (Mathf.Abs(transform.position.x - point.position.x) > DeltaPointDistance)
                {
                    if (IsPatrol)
                    {
                        _enemyAnimator.PlayWalk();
                        MoveAlongXAxis(negativeDirection);
                        yield return null;
                    }
                    else
                    {
                        yield break;
                    }
                }
                _enemyAnimator.PlayIdle();
                yield return waitForSeconds;
            }
        }
    }

    private void MoveAlongXAxis(bool negativeDirection = false)
    {
        LookInXDirection(negativeDirection);
        float _xShift = _walkingSpeed * Time.deltaTime * (negativeDirection ? -1 : 1);
        transform.Translate(_xShift, 0, 0);
    }

    private void LookInXDirection(bool negativeDirection)
    {
        int direction = negativeDirection ? -1 : 1;

        if (transform.localScale.x * direction > 0)
            return;

        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnValidate()
    {
        if (_walkingSpeed < 0)
            _walkingSpeed *= -1;
    }
}
