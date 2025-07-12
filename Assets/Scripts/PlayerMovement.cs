using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    enum PlayerState { Running, Airborne, Sliding, Dead }
    PlayerState _state;

    public Rigidbody2D Body;
    public float Speed;
    public BoxCollider2D PlayerCollider;
    public SpriteRenderer SpriteRend;
    [SerializeField] PlayerInput _input;

    [SerializeField] BoxCollider2D _groundCheck;
    [SerializeField] LayerMask _groundMask;

    [SerializeField] float _gravityFloatMult;
    [SerializeField] float _gravityFallMult;
    [SerializeField] float _gravity;
    public float JumpPower;
    [SerializeField] float _maxJumpSpeed;       // Remember to set as a multiplier of JumpPower post testing
    [SerializeField] float _floatJumpSpeed;     // Remember to set as a multiplier of JumpPower post testing
    [SerializeField] float _coyoteTime;
    float _coyoteTimer;

    bool _grounded;

    [SerializeField] float _slideTime;
    float _slideTimer;
    bool _sliding;

    [SerializeField] float _shiftTime;
    float _shiftTimer;
    public bool Shifting;

    bool _stateComplete;

    public Animator Animator;
    public Animator ColliderAnimator;

    public ParticleSystem DeathParticles;
    void Update()
    {
        if (_state != PlayerState.Dead)
        {
            Body.linearVelocityX = Speed;
            if (_stateComplete)
            {
                SelectState();
            }
            UpdateState();
            if (Shifting)
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
        if (_state == PlayerState.Dead)
        {
            return;
        }
        _stateComplete = false;
        if (!_grounded)
        {
            _state = PlayerState.Airborne;
            StartAirborne();
        }
        else
        {
            if (_sliding)
            {
                _state = PlayerState.Sliding;
                StartSliding();
            }
            else
            {
                _state = PlayerState.Running;
                StartRunning();
            }
        }
    }

    void StartAirborne()
    {
        Animator.Play("Jump");
    }

    void StartSliding()
    {
        Animator.Play("Slide");
    }

    void StartRunning()
    {
        Animator.Play("Run");
    }

    void UpdateState()
    {
        switch (_state)
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
        if (!_grounded || _sliding)
        {
            _stateComplete = true;
        }
    }

    void UpdateAir()
    {
        if (_grounded)
        {
            _stateComplete = true;
        }
    }

    void UpdateSlide()
    {
        _slideTimer += Time.deltaTime;
        if (!_grounded || _slideTimer > _slideTime)
        {
            _stateComplete = true;
            _sliding = false;
            ColliderAnimator.SetTrigger("Idle");
        }
    }

    void Shift()
    {
        SpriteRend.color = Color.white;
        _shiftTimer += Time.deltaTime;
        if (_shiftTimer > _shiftTime)
        {
            _stateComplete = true;
            Shifting = false;
            SpriteRend.color = Color.black;
            _shiftTimer = 0;
        }
    }


    IEnumerator PlayerDeath()
    {
        Animator.Play("Death");
        _state = PlayerState.Dead;
        _input.enabled = false;
        yield return new WaitForSeconds(1.5f);
        SpriteRend.enabled = false;
        DeathParticles.Play();
    }
    void HandleJump()
    {
        if (_grounded)
        {
            _coyoteTimer = _coyoteTime;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }
        // If at peak of jump, lower gravity; floatier jump
        if (Mathf.Abs(Body.linearVelocityY) < _floatJumpSpeed)
        {
            Body.gravityScale = _gravity * _gravityFloatMult;
        }

        // Strengthen gravity if going downwards
        else if (Body.linearVelocityY < -_floatJumpSpeed)
        {
            Body.gravityScale = _gravity * _gravityFallMult;
        }

        else
            Body.gravityScale = _gravity;

        // Limit fall speed
        Body.linearVelocityY = Mathf.Max(Body.linearVelocityY, -_maxJumpSpeed);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        // Full jump
        if (ctx.performed && (_grounded || _coyoteTimer > 0))
        {
            Body.linearVelocityY = JumpPower;
            _coyoteTimer = 0f;
        }

        // Partial jump
        if (ctx.canceled && Body.linearVelocityY > 0)
        {
            Body.linearVelocityY *= 0.5f;
            _coyoteTime = 0f;
        }
    }

    public void Slam(InputAction.CallbackContext ctx)
    {
        // Slam down
        if (ctx.performed)
        {
            if (!_grounded)
            {
                Body.linearVelocityY = -_maxJumpSpeed;
            }
            else
            {
                _slideTimer = 0;
                ColliderAnimator.SetTrigger("Slide");
                _sliding = true;
            }
        }
    }

    public void Shift(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !Shifting)
        {
            Shifting = true;
        }
    }

    void CheckGround()
    {
        // Allows jumping if slightly off the grouond, or if not yet touching the ground.
        _grounded = Physics2D.OverlapAreaAll(_groundCheck.bounds.min, _groundCheck.bounds.max, _groundMask).Length > 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PShift") && !Shifting)
        {
            StartCoroutine("PlayerDeath");
        }
        else if (collision.CompareTag("Obstacle"))
        {
            StartCoroutine("PlayerDeath");
        }
    }
}
