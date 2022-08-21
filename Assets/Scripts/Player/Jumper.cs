using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpFor�e;

    private const float JumpFor�eScale = 0.1f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        Vector2 force = _jumpFor�e * JumpFor�eScale * Vector2.up;
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
