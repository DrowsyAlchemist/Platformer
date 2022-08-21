using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Fruit : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 1;
    [SerializeField] private UnityEvent _isCollected;

    private void Start()
    {
        if (GetComponent<Collider2D>().isTrigger == false)
            throw new System.Exception("Collider should be trigger.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            _isCollected.Invoke();
            Destroy(gameObject, _destroyDelay);
        }
    }

    private void OnValidate()
    {
        if (_destroyDelay < 0)
            _destroyDelay *= -1;
    }
}
