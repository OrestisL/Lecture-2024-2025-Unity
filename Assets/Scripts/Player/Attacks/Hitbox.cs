using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        // do damage
        // make some generic health component, use get component for it and do damage
        // for now jsut print name
        Debug.Log(other.gameObject.name);
        Destroy(other.gameObject);
    }
}
