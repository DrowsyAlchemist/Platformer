using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForñe;

    private const float JumpForñeScale = 0.1f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForñe * JumpForñeScale);
    }
}
