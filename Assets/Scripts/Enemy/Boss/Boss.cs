using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] float _walkingSpeed = 2;

    public void WakeUp()
    {
        Player player = FindObjectOfType<Player>();

        if (player.IsAlive)
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
