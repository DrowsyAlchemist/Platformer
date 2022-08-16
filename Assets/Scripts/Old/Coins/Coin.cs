using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private UnityEvent _isCollected;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _isCollected.Invoke();
    }
}
