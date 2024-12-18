using UnityEngine;

public class SampleBehavior : ManagedBehavior
{
    private float _radius;
    private float _velocity;

    private float _time;

    private void Start()
    {
        _radius = Random.Range(1.0f, 8.0f);
        _velocity = Random.Range(1.0f, 5.0f);
    }

    public override void OnFixedUpdate()
    {
        Debug.Log($"{name}: FixedUpdate!");
    }

    public override void OnLateUpdate()
    {
        Debug.Log($"{name}: LateUpdate!");
    }

    public override void OnUpdate()
    {
        _time += Time.deltaTime;
        float x = _radius * Mathf.Cos(_time * _velocity);
        float y = _radius * Mathf.Sin(_time * _velocity);

        transform.position = new Vector3(x, y, 0);
    }
}
