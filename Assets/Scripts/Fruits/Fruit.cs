using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Fruit : MonoBehaviour
{
    [SerializeField] private UnityEvent _isCollected;

    private const string CollectedAnimation = "Collected";
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            _animator.SetTrigger(CollectedAnimation);
            Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void OnDestroy()
    {
        _isCollected.Invoke();
    }
}
