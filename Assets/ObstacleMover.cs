using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _obsLength;
    [SerializeField] float _spawnInterval;
    private float _timer;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocityX = -(_obsLength/_spawnInterval);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 4*_spawnInterval)
        {
            Destroy(gameObject);
        }
    }
}
