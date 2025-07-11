using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;
    [SerializeField] GameObject _laser;
    [SerializeField] bool _pink;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerMovement>();        
    }

    void Update()
    {
        if (_pink)
        {
            _laser.SetActive(!_player.Shifting);
        }
        else
        {
            _laser.SetActive(_player.Shifting);
        }
    }
}
