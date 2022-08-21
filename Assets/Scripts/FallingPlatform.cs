using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class FallingPlatform : MonoBehaviour, IRestartable
{
    [SerializeField] private float _fallingDelay = 2;
    [SerializeField] private float _fallingSpeed = 2;
    [SerializeField] private float _fallingHeight = 2;

    private Vector3 _initialPosition;
    private bool _isFalling;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _initialPosition = transform.position;
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Restart()
    {
        transform.position = _initialPosition;
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            if (_isFalling == false)
                StartCoroutine(FallWithDelay());
    }

    private IEnumerator FallWithDelay()
    {
        _isFalling = true;
        yield return new WaitForSeconds(_fallingDelay);
        _collider.enabled = false;
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        float targetYPosition = transform.position.y - _fallingHeight;

        while (transform.position.y > targetYPosition)
        {
            transform.Translate(_fallingSpeed * Time.deltaTime * Vector3.down);
            yield return null;
        }
        _spriteRenderer.enabled = false;
        _isFalling = false;
    }
}