using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    float startTime;
    public float time => Time.time - startTime;
    public virtual void Enter()
    {

    }
    public virtual void Do()
    {

    }
    public virtual void FixedDo()
    {

    }
    public virtual void Exit()
    {

    }
}


