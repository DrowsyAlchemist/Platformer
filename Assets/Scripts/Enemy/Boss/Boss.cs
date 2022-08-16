using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] float _walkingSpeed = 2;

    private const float DeltaPlayerDistance = 0.1f;

    public void Wake()
    {
        Player player = FindObjectOfType<Player>();

        if (player.IsAlive)
            StartCoroutine(MoveToPlayer(player));
    }

    private IEnumerator MoveToPlayer(Player player)
    {
        while (player.IsAlive)
        {
            bool negativeDirection = (transform.position.x - player.transform.position.x) > 0;

            if (IsDizzy == false)
            {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) > DeltaPlayerDistance)
                    MoveAlongXAxis(negativeDirection);
                else
                    EnemyAnimator.PlayIdle();
            }

            yield return null;
        }
    }

    private void MoveAlongXAxis(bool negativeDirection = false)
    {
        EnemyAnimator.PlayWalk();
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
}
