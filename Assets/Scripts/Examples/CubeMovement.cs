using UnityEngine;

public class CubeMovement : ManagedBehavior
{
    float number = 0;
    public override void OnFixedUpdate()
    {
        number += Time.fixedDeltaTime;
        Debug.Log(number.ToString("F2"));
    }

    public override void OnLateUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        transform.position +=  0.01f * Mathf.Sin(Time.time * 0.5f) * transform.right;   
    }
}
