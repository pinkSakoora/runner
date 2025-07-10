using UnityEngine;

public class PLaserManager : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;
    [SerializeField] GameObject _laser;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerMovement>();        
    }

    void Update()
    {
        _laser.SetActive(!_player.shifting);
    }
}
