using System.Collections;
using UnityEngine;

public class FruitsSpawner : MonoBehaviour
{
    [SerializeField] private Fruit _template;
    [SerializeField] private uint _fruitsCount;
    [SerializeField] private float _spawnDelayInSeconds = 1;
    [SerializeField] private bool _isRandomOrder;

    private Transform[] _swawnPoints;

    private void Start()
    {
        InitializeSpawnPoints();
        StartCoroutine(Spawn());
    }

    private void InitializeSpawnPoints()
    {
        if (transform.childCount < 1)
            throw new System.Exception("There are no spawn points.");

        _swawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < _swawnPoints.Length; i++)
            _swawnPoints[i] = transform.GetChild(i);

        if (_isRandomOrder)
            Shuffle(_swawnPoints);
    }

    private IEnumerator Spawn()
    {
        var waitForSeconds = new WaitForSeconds(_spawnDelayInSeconds);

        for (int i = 0; i < _fruitsCount; i++)
        {
            Instantiate(_template, _swawnPoints[i].transform.position, Quaternion.identity);
            yield return waitForSeconds;
        }
    }

    private void Shuffle(Transform[] points)
    {
        for (int i = points.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = points[j];
            points[j] = points[i];
            points[i] = temp;
        }
    }

    private void OnValidate()
    {
        if (_fruitsCount > transform.childCount)
            _fruitsCount = (uint)transform.childCount;
    }
}