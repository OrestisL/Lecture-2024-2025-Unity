using UnityEngine;
using UnityEngine.UIElements;

public class DartMovement : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float Force, Angle;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Quaternion rotation = Quaternion.AngleAxis(Angle, transform.right);
        Rigidbody.AddForce(rotation * (Force * Vector3.forward), ForceMode.VelocityChange);
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
}
