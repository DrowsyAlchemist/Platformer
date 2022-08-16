using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] UnityEvent _landed;

    public bool IsOnGround { get; private set; }

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_layerMask & (1 << collision.gameObject.layer)) > 0)
        {
            IsOnGround = true;
            _landed.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.GetComponent<BoxCollider2D>().IsTouchingLayers(_layerMask))
            return;

        if ((_layerMask & (1 << collision.gameObject.layer)) > 0)
            IsOnGround = false;
    }
}
