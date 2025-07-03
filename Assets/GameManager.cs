using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] _obstaclePrefab;

    public void SpawnObstacle()
    {
        Instantiate(_obstaclePrefab[Random.Range(0,_obstaclePrefab.Count())], gameObject.transform.position, Quaternion.identity);
    }
}
