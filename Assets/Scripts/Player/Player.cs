using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour, IRestartable
{
    [SerializeField] private UnityEvent _died;

    private PlayerMovement _movement;
    private PlayerAnimator _playerAnimator;
    private Vector3 _restartPosition;

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
        _restartPosition = transform.position;
    }

    public void Restart()
    {
        CreateGrave();
        transform.position = _restartPosition;
        _playerAnimator.PlayIdle();
        IsAlive = true;
    }

    private void CreateGrave()
    {
        if (TryGetComponent(out GraveCreator graveCreator))
            graveCreator.Create();
    }

    private void Update()
    {
        if (IsAlive)
            _movement.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (enemy.IsDizzy == false)
            {
                TurnToEnemy(enemy);
                Die();
            }
        }
    }

    private void TurnToEnemy(Enemy enemy)
    {
        bool negativeDirection = (transform.position.x - enemy.transform.position.x) > 0;
        transform.LookInXDirection(negativeDirection);
    }

    private void Die()
    {
        if (IsAlive)
        {
            IsAlive = false;
            _playerAnimator.PlayDie();
            _died.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Checkpoint checkpoint))
            if (checkpoint.Reached == false)
                _restartPosition = checkpoint.transform.position;
    }
}
