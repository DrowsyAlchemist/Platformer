using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private float _walkingSpeed = 2;

    public void ChasePlayer(Player player)
    {
        if (player.IsAlive && IsDizzy == false)
            StartCoroutine(MoveToPlayer(player));
    }

    private IEnumerator MoveToPlayer(Player player)
    {
        EnemyAnimator.PlayWalk();

        while (player.IsAlive)
        {
            if (IsDizzy == false)
                transform.MoveAlongXAxis(player.transform, _walkingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
