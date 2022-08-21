using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private Collider2D _collider;

    public bool IsOnGround { get; private set; }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_groundLayer & (1 << collision.gameObject.layer)) > 0)
            IsOnGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collider.IsTouchingLayers(_groundLayer))
            return;

        if ((_groundLayer & (1 << collision.gameObject.layer)) > 0)
            IsOnGround = false;
    }

    private void OnValidate()
    {
        if (GetComponent<Collider2D>().isTrigger == false)
            GetComponent<Collider2D>().isTrigger = true;
    }
}
