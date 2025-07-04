using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    /*
        Generate map of a certain length according to parameters in PermittedList
        Always spawn 2-3 Ground blocks first
        After winCondition number of tiles have been generated, instantiate winBlock.
        Spawn blocks until winCondition number of blocks have been generated.
    */
    public int WinCondition;
    private PermittedList _permittedList;
    [SerializeField] Vector3 _spawnPoint;
    [SerializeField] float _obstacleLength;
    [SerializeField] GameObject _winBlock;
    private GameObject _prevObstacle;
    void Start()
    {
        _permittedList = GetComponent<PermittedList>();
        _prevObstacle = _permittedList.ground;
        GenerateMap();
    }
    void GenerateMap()
    {
        _spawnPoint = new Vector3(0, -5, 0);
        for (int i = 0; i < 2; i++)
        {
            Instantiate(_permittedList.ground, _spawnPoint, Quaternion.identity);
            _spawnPoint += new Vector3(_obstacleLength, 0, 0);
        }
        for (int i = 0; i < WinCondition; i++)
        {
            var potentialObstacles = _permittedList.permittedList[_prevObstacle];
            GameObject currentBlock = potentialObstacles[Random.Range(0, potentialObstacles.Count())];
            Instantiate(currentBlock, _spawnPoint, Quaternion.identity);
            _prevObstacle = currentBlock;
            _spawnPoint += new Vector3(_obstacleLength, 0, 0);
        }
        Instantiate(_winBlock, _spawnPoint, Quaternion.identity);
    }
}
