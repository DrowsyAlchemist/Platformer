
using System.Collections;
using UnityEngine;
using System.Linq;

public class FruitsSpawner : MonoBehaviour
{
    [SerializeField] private Fruit _fruit;
    [SerializeField] private uint _fruitsCount;
    [SerializeField] private float _spawnDelayInSeconds;
    [SerializeField] private bool _isRandomOrder;

    private Transform[] _swawnPoints;

    private void Start()
    {
        if (gameObject.transform.childCount < 1)
            throw new System.Exception("There are no spawn points.");

        _swawnPoints = new Transform[gameObject.transform.childCount];

        for (int i = 0; i < _swawnPoints.Length; i++)
            _swawnPoints[i] = gameObject.transform.GetChild(i).transform;

        if (_isRandomOrder)
            Shuffle(_swawnPoints);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var waitForSeconds = new WaitForSeconds(_spawnDelayInSeconds);

        for (int i = 0; i < _fruitsCount; i++)
        {
            Instantiate(_fruit, _swawnPoints[i].transform.position, Quaternion.identity);
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
        if (_fruitsCount > gameObject.transform.childCount)
            _fruitsCount = (uint)gameObject.transform.childCount;
    }
}
