using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] float _length;
    private float _timer;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocityX = -_speed;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _length/_speed*4)
        {
            Destroy(gameObject);
        }
    }
}
