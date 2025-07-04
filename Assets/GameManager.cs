using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PermittedList _permittedList;
    private GameObject _prevObstacle;

    void Start()
    {
        _permittedList = GetComponent<PermittedList>();
        _prevObstacle = _permittedList.ground;
    }

    public void SpawnObstacle()
    {
        var potentialObstacles = _permittedList.permittedList[_prevObstacle];
        GameObject currentBlock = potentialObstacles[Random.Range(0, potentialObstacles.Count())];
        Instantiate(currentBlock, transform.position, Quaternion.identity);
        _prevObstacle = currentBlock;
    }
}
