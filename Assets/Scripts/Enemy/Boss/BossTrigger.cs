using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class BossTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _playerEntered;

    private void OnEnable()
    {
        if (GetComponent<Collider2D>().isTrigger == false)
            throw new System.Exception("Collider should be trigger.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player  _))
            _playerEntered.Invoke();
    }
}
