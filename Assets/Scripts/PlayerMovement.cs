using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float Speed;
    public float JumpPower;

    [SerializeField] BoxCollider2D _groundCheck;
    [SerializeField] LayerMask _groundMask;

    [SerializeField] float _gravityFloatMult;
    [SerializeField] float _gravityFallMult;
    [SerializeField] float _gravity;

    [SerializeField] float _maxJumpSpeed;       // Remember to set as a multiplier of JumpPower post testing
    [SerializeField] float _floatJumpSpeed;     // Remember to set as a multiplier of JumpPower post testing

    bool grounded;

    void Update()
    {
        _rb.linearVelocityX = Speed;
        HandleJump();
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    void HandleJump()
    {
        // If at peak of jump, lower gravity; floatier jump
        if (Mathf.Abs(_rb.linearVelocityY) < _floatJumpSpeed)
        {
            _rb.gravityScale = _gravity * _gravityFloatMult;
        }

        // Strengthen gravity if going downwards
        else if (_rb.linearVelocityY < -_floatJumpSpeed)
        {
            _rb.gravityScale = _gravity * _gravityFallMult;
        }

        else
            _rb.gravityScale = _gravity;

        // Limit fall speed
        _rb.linearVelocityY = Mathf.Max(_rb.linearVelocityY, -_maxJumpSpeed);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        // Full jump
        if (ctx.performed && grounded)
        {
            _rb.linearVelocityY = JumpPower;
        }

        // Partial jump
        if (ctx.canceled && _rb.linearVelocityY > 0)
        {
            _rb.linearVelocityY *= 0.5f;
        }
    }

    public void Slam(InputAction.CallbackContext ctx)
    {
        // Slam down
        if (ctx.performed && !grounded)
        {
            _rb.linearVelocityY = -_maxJumpSpeed;
        }
    }

    void CheckGround()
    {
        // Allows jumping if slightly off the grouond, or if not yet touching the ground.
        grounded = Physics2D.OverlapAreaAll(_groundCheck.bounds.min, _groundCheck.bounds.max, _groundMask).Length > 0;
    }
}
