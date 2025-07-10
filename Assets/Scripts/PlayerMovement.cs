using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    enum PlayerState { Running, Airborne, Sliding, Dead }
    PlayerState state;

    public Rigidbody2D _rb;
    public float Speed;
    public float JumpPower;
    public BoxCollider2D playerCollider;
    public SpriteRenderer spriteRend;
    [SerializeField] PlayerInput input;

    [SerializeField] BoxCollider2D _groundCheck;
    [SerializeField] LayerMask _groundMask;

    [SerializeField] float _gravityFloatMult;
    [SerializeField] float _gravityFallMult;
    [SerializeField] float _gravity;

    [SerializeField] float _maxJumpSpeed;       // Remember to set as a multiplier of JumpPower post testing
    [SerializeField] float _floatJumpSpeed;     // Remember to set as a multiplier of JumpPower post testing

    bool grounded;

    float slideTime;
    bool sliding;

    float shiftTime;
    public bool shifting;

    bool stateComplete;

    public Animator animator;
    public Animator colliderAnimator;
    void Update()
    {
        if (state != PlayerState.Dead)
        {
            _rb.linearVelocityX = Speed;
            if (stateComplete)
            {
                SelectState();
            }
            UpdateState();
            if (shifting)
            {
                Shift();
            }
        }
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleJump();
    }

    void SelectState()
    {
        if (state == PlayerState.Dead)
        {
            return;
        }
        stateComplete = false;
        if (!grounded)
        {
            state = PlayerState.Airborne;
            StartAirborne();
        }
        else
        {
            if (sliding)
            {
                state = PlayerState.Sliding;
                StartSliding();
            }
            else
            {
                state = PlayerState.Running;
                StartRunning();
            }
        }
    }

    void StartAirborne()
    {
        animator.Play("Jump");
    }

    void StartSliding()
    {
        animator.Play("Slide");
    }

    void StartRunning()
    {
        animator.Play("Run");
    }

    void UpdateState()
    {
        switch (state)
        {
            case PlayerState.Running:
                UpdateRun();
                break;
            case PlayerState.Airborne:
                UpdateAir();
                break;
            case PlayerState.Sliding:
                UpdateSlide();
                break;
        }
    }

    void UpdateRun()
    {
        if (!grounded || sliding)
        {
            stateComplete = true;
        }
    }

    void UpdateAir()
    {
        if (grounded)
        {
            stateComplete = true;
        }
    }

    void UpdateSlide()
    {
        slideTime += Time.deltaTime;
        if (!grounded || slideTime > 1f)
        {
            stateComplete = true;
            sliding = false;
            colliderAnimator.SetTrigger("Idle");
        }
    }

    void Shift()
    {
        spriteRend.color = Color.white;
        shiftTime += Time.deltaTime;
        if (shiftTime > 1f)
        {
            stateComplete = true;
            shifting = false;
            spriteRend.color = Color.black;
            shiftTime = 0;
        }
    }

    void Death()
    {
        animator.speed = 0;
        state = PlayerState.Dead;
        _rb.linearVelocityX = 0;
        _rb.gravityScale = 0;
        input.enabled = false;

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
        if (ctx.performed)
        {
            if (!grounded)
            {
                _rb.linearVelocityY = -_maxJumpSpeed;
            }
            else
            {
                slideTime = 0;
                colliderAnimator.SetTrigger("Slide");
                sliding = true;
            }
        }
    }

    public void Shift(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !shifting)
        {
            shifting = true;
        }
    }

    void CheckGround()
    {
        // Allows jumping if slightly off the grouond, or if not yet touching the ground.
        grounded = Physics2D.OverlapAreaAll(_groundCheck.bounds.min, _groundCheck.bounds.max, _groundMask).Length > 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PShift") && !shifting)
        {
            Death();
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Death();
        }
    }
}
