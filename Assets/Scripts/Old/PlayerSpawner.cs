using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _template;

    private const float SpawnDelay = 2f;
    private Checkpoint[] _checkpoints;
    private GameFinishTrigger _gameFinishTrigger;

    private void OnEnable()
    {
        _checkpoints = GetComponentsInChildren<Checkpoint>();

        if (_checkpoints.Length == 0)
            throw new System.Exception("There are no checkpoints.");

        _gameFinishTrigger = FindObjectOfType<GameFinishTrigger>();

        if (_gameFinishTrigger == null)
            throw new System.Exception("There are no GameFinishTrigger.");

        _gameFinishTrigger.PlayerLost += OnPlayerLost;
    }

    private Checkpoint GetLastReachedCheckpoint()
    {
        for (int i = _checkpoints.Length - 1; i >= 0; i--)
            if (_checkpoints[i].Reached)
                return _checkpoints[i];

        throw new System.Exception("There are no reached checkpoints.\n");
    }

    private void OnPlayerLost()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnDelay);
        var lastReachedCheckpoint = GetLastReachedCheckpoint();

        if (FindObjectOfType<Player>() == null)
            Instantiate(_template, lastReachedCheckpoint.transform.position, Quaternion.identity);
    }
}
