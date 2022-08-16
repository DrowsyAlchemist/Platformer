using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent _died;
    [SerializeField] private Grave _template;

    private const float DestroyDelay = 1;
    private PlayerMovement _movement;
    private PlayerAnimator _playerAnimator;

    public event UnityAction Died
    {
        add => _died.AddListener(value);
        remove => _died.RemoveListener(value);
    }

    public bool IsAlive { get; private set; } = true;

    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (IsAlive)
            _movement.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy) && collision.collider.isTrigger == false)
            if (enemy.IsDizzy == false)
                StartCoroutine(DieAndDestroy());
    }

    private IEnumerator DieAndDestroy()
    {
        if (IsAlive)
        {
            Debug.Log("Game over! You lose.");
            IsAlive = false;
            _died.Invoke();
            _playerAnimator.PlayDieAnimation();
            yield return new WaitForSeconds(DestroyDelay);
            CreateGrave();
            Destroy(gameObject);
        }
    }

    private void CreateGrave()
    {
        if (_template != null)
            Instantiate(_template, transform.position, Quaternion.identity);
    }
}
