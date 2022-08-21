using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] LayerMask _groundLayer;

    private Collider2D _collider;

    public bool IsOnGround { get; private set; }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        if (_collider.isTrigger == false)
            throw new System.Exception("Collider should be trigger.");
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
}
