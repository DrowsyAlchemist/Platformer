using UnityEngine;

public class GraveCreator : MonoBehaviour
{
    [SerializeField] private Grave _template;
    [SerializeField] private float _spawnHeight = 0.75f;

    public void Create()
    {
        Vector3 position = transform.position + _spawnHeight * Vector3.up;
        Instantiate(_template, position, Quaternion.identity);
    }
}
