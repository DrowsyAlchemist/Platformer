using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _playerWin;
    [SerializeField] private UnityEvent _playerLost;
    [SerializeField] private float _playerLostDelay = 1;
    [SerializeField] private float _restartDelay = 4;

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
        FindObjectOfType<Player>().Died += OnPlayerDied;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            _playerWin.Invoke();
    }

    private void OnPlayerDied()
    {
        StartCoroutine(InvokePlayerLost());
        StartCoroutine(Restart());
    }

    private IEnumerator InvokePlayerLost()
    {
        yield return new WaitForSeconds(_playerLostDelay);
        _playerLost.Invoke();
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(_restartDelay);
        var gameObjects = FindObjectsOfType<GameObject>();

        foreach (var gameObject in gameObjects)
            if (gameObject.TryGetComponent(out IRestartable restartable))
                restartable.Restart();

        var restartable = FindObjectsOfType<IRestartable>();
    }

    private void OnValidate()
    {
        if (_playerLostDelay < 0)
            _playerLostDelay *= -1;

        if (_restartDelay < 0)
            _restartDelay *= -1;

        if (_playerLostDelay > _restartDelay)
            _playerLostDelay = _restartDelay;
    }
}
