using UnityEngine;

public class Score : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 contactPoint = collision.GetContact(0).point;
        Vector3 direction = contactPoint - transform.position;
        float radius = Vector3.Distance(transform.position, contactPoint);

        Vector3 zeroAngleAxis = transform.right;
        float angle = Vector3.SignedAngle(zeroAngleAxis, direction, transform.up);
        Debug.Log($"radius: {radius}, angle: {angle}");

    }

}
