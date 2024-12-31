using UnityEngine;

public class CleanupObject : MonoBehaviour
{
    private float _lifetime = 15.0f;

    public void Init(float lifetime)
    {
        if (lifetime <= 0)
            _lifetime = 15.0f;
        else
            _lifetime = lifetime;

        Destroy(gameObject, _lifetime);
    }
}
