using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class BossTrigger : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private UnityEvent _playerEntered;

    private void OnEnable()
    {
        if (_boss == null)
            throw new System.Exception("Boss is not assigned.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _boss.ChasePlayer(player);
            _playerEntered.Invoke();
        }
    }

    private void OnValidate()
    {
        if (GetComponent<Collider2D>().isTrigger == false)
            GetComponent<Collider2D>().isTrigger = true;
    }
}
