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
            if (_groundChecker.IsOnGround)
                _jumper.Jump();

        if (Input.GetKey(KeyCode.D))
            MoveAlongXAxis();
        else if (Input.GetKey(KeyCode.A))
            MoveAlongXAxis(negativeDirection: true);
        else
            _playerAnimator.PlayIdle();

        if (_groundChecker.IsOnGround == false)
            _playerAnimator.PlayJump();
    }

    private void MoveAlongXAxis(bool negativeDirection = false)
    {
        _playerAnimator.PlayRun();
        transform.MoveAlongXAxis(_walkingSpeed * Time.deltaTime, negativeDirection);
    }

    private void OnValidate()
    {
        if (_walkingSpeed < 0)
            _walkingSpeed *= -1;
    }
}
