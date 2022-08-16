using UnityEngine;
using UnityEngine.Events;

public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _playerWin;
    [SerializeField] private UnityEvent _playerLost;

    private Enemy[] _enemies;

    public event UnityAction PlayerWin
    {
        add => _playerWin.AddListener(value);
        remove => _playerWin.RemoveListener(value);
    }

    public event UnityAction PlayerLost
    {
        add => _playerLost.AddListener(value);
        remove => _playerLost.RemoveListener(value);
    }

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();

        if (_enemies.Length == 0)
            throw new System.Exception("There are no enemies.");

        foreach (var enemy in _enemies)
        {
            enemy.IsWin += OnEnemyWin;
            PlayerWin += enemy.OnPlayerWin;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            _playerWin.Invoke();
    }

    private void OnEnemyWin()
    {
        _playerLost.Invoke();
    }
}
