using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] float _jumpPower;
    [SerializeField] GameManager gameManager;

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundMask);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
            _rb.linearVelocityY = _jumpPower;
        if (context.canceled && _rb.linearVelocityY > 0f)
        {
            _rb.linearVelocityY = _jumpPower * 0.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
            gameManager.SpawnObstacle();
        if (collision.gameObject.CompareTag("Fall"))
            Destroy(gameObject);
    }
}
