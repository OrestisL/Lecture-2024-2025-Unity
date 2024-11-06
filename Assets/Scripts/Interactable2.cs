using UnityEngine;

public class Interactable2 : BaseInteractable
{
    public GameObject Particle;
    public override void Interact()
    {
        Instantiate(Particle, transform.position + Vector3.up, Quaternion.identity);
        transform.position += (Random.Range(-10f, 10f) * Vector3.right + Random.Range(-10f, 10f) * Vector3.forward);
    }
}
