using UnityEngine;

public abstract class ManagedBehavior : MonoBehaviour
{
    public virtual void OnEnable() 
    {
        BehaviorManager.Instance.Add(this);
    } 
    public virtual void Start() 
    {
        BehaviorManager.Instance.Add(this);
    }
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnLateUpdate();
    public virtual void OnDisable() 
    {
        BehaviorManager.Instance.Remove(this);
    }
}
