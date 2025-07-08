using System.Collections.Generic;
using UnityEngine;

public class PermittedList : MonoBehaviour
{
    public Dictionary<GameObject, List<GameObject>> permittedList = new Dictionary<GameObject, List<GameObject>>();
    [SerializeField] public GameObject ground;
    [SerializeField] GameObject jjv;
    [SerializeField] GameObject jvseries;
    [SerializeField] GameObject ujvjj;

    void Awake()
    {
        List<GameObject> groundList = new List<GameObject>()
        {
            ground,
            jjv,
            jvseries,
            ujvjj
        };

        List<GameObject> jjvList = new List<GameObject>()
        {
            ground,
            ujvjj
        };

        List<GameObject> jvseriesList = new List<GameObject>()
        {
            jjv,
            ground
        };

        List<GameObject> ujvjjList = new List<GameObject>()
        {
            ground,
            jjv,
            jvseries
        };
        permittedList.Add(ground, groundList);
        permittedList.Add(jjv, jjvList);
        permittedList.Add(jvseries, jvseriesList);
        permittedList.Add(ujvjj, ujvjjList);
    }
}
