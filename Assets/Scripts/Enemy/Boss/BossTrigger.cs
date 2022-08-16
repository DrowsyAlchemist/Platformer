using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _playerEntered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player  _))
            _playerEntered.Invoke();
    }
}
