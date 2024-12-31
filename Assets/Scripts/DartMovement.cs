using UnityEngine;


[RequireComponent (typeof(Rigidbody), typeof(MeshCollider))]
public class DartMovement : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float Force;

    public void Init(float force)
    {
        Force = force;

        float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, transform.right);

        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        Rigidbody.AddForce(rotation * (Force * Vector3.forward), ForceMode.VelocityChange);

        GetComponent<MeshCollider>().convex = true;
    }

    private void FixedUpdate()
    {
        RotateTowardsDirection();
    }

    private void RotateTowardsDirection(bool immediate = false)
    {
        if (Rigidbody.linearVelocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Rigidbody.linearVelocity.normalized, Vector3.up);

            if (immediate)
            {
                transform.rotation = targetRotation;
            }
            else
            {
                float angle = Vector3.Angle(transform.forward, Rigidbody.linearVelocity.normalized);
                float lerpFactor = angle * Time.deltaTime; // Use the angle as the interpolation factor
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpFactor);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this);
        Destroy(Rigidbody);
    }
}
