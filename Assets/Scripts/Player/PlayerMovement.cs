using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Jumper))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _walkingSpeed = 2;

    private PlayerAnimator _playerAnimator;
    private Jumper _jumper;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _jumper = GetComponent<Jumper>();
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_groundChecker.IsOnGround)
            {
                _playerAnimator.StartJumpAnimation();
                _jumper.Jump();
            }
        }
        if (Input.GetKey(KeyCode.D))
            MoveAlongXAxis();
        else if (Input.GetKey(KeyCode.A))
            MoveAlongXAxis(negativeDirection: true);
        else
            _playerAnimator.StopRunAnimation();
    }

    private void MoveAlongXAxis(bool negativeDirection = false)
    {
        _playerAnimator.StartRunAnimation();
        LookInXDirection(negativeDirection);
        float _xShift = _walkingSpeed * Time.deltaTime * (negativeDirection ? -1 : 1);
        transform.Translate(_xShift, 0, 0);
    }

    private void LookInXDirection(bool negativeDirection)
    {
        int direction = negativeDirection ? -1 : 1;

        if (transform.localScale.x * direction > 0)
            return;

        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnValidate()
    {
        if (_walkingSpeed < 0)
            _walkingSpeed *= -1;
    }
}
