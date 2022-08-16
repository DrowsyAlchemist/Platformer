using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float _fallingDelay = 2;
    [SerializeField] private float _fallingSpeed = 2;
    [SerializeField] private float _fallingHeight = 2;

    private const float RestartDelay = 2;
    private Vector3 _initialPosition;
    private GameFinishTrigger _gameFinishTrigger;
    private bool _isFalling;

    private void OnEnable()
    {
        _initialPosition = transform.position;
        _gameFinishTrigger = FindObjectOfType<GameFinishTrigger>();

        if (_gameFinishTrigger == null)
            throw new System.Exception("There are no GameFinishTrigger.");

        _gameFinishTrigger.PlayerLost += OnPlayerLost;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            if (_isFalling == false)
            {
                StartCoroutine(FallWithDelay(_fallingDelay));
                _isFalling = true;
            }
        }
    }

    private IEnumerator FallWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        float targetYPosition = transform.position.y - _fallingHeight;

        while (transform.position.y > targetYPosition)
        {
            Fall();
            yield return null;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        _isFalling = false;
    }

    private void Fall()
    {
        transform.Translate(_fallingSpeed * Time.deltaTime * Vector3.down);
    }

    private void OnPlayerLost()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(RestartDelay);
        transform.position = _initialPosition;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
