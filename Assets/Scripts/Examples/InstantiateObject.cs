using System;
using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    public PrimitiveType PrimitiveType = PrimitiveType.Cube;
    public float PrimitiveLifetime = 10.0f;
    public float ForceAmount = 100.0f;
    public float MaxAngle = 40.0f;
    public float MinAngle = 30.0f;

    public int DestroyAfterCollisions = 5;
    private int m_currentCollisionNumber = 0;
    private void OnCollisionEnter(Collision collision)
    {
        float angle = 180 - UnityEngine.Random.Range(MinAngle, MaxAngle);

        PrimitiveType = (PrimitiveType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PrimitiveType)).Length - 3);
        GameObject primitive = GameObject.CreatePrimitive(PrimitiveType);

        //set position
        Vector3 position = collision.GetContact(0).point + Vector3.up * primitive.transform.localScale.y * 0.5f;

        Rigidbody primBody = primitive.AddComponent<Rigidbody>();

        //calculate direction vector
        GameObject other = collision.gameObject;
        Vector3 otherUpwards = other.transform.up;
        Quaternion rotation = Quaternion.AngleAxis(UnityEngine.Random.value > 0.5f ? angle : -angle, other.transform.forward);
        Vector3 direction = rotation * otherUpwards;

        //apply force 
        primBody.AddForce(direction * ForceAmount, ForceMode.Acceleration);

        //destroy primitive
        Destroy(primitive, PrimitiveLifetime);

        m_currentCollisionNumber++;
        if (m_currentCollisionNumber >= DestroyAfterCollisions)
            Destroy(gameObject);
    }
}
