using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 15.0f;

    public float maxZoomOut = 25.0f;
    public float maxZoomIn = 0.0f;
    [Range(0.5f, 15f)]
    public float scrollWheelSensitivity = 1.0f;

    private Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;

    public float distance;

    public LayerMask obstacleLayer;

    private InputAction _zoomAction;

    void Start()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
        _zoomAction = InputSystem.actions.FindAction("Zoom");
    }

    public void Update()
    {
        float zoom = _zoomAction.ReadValue<Vector2>().y;
        Zoom(zoom);
    }

    public void FixedUpdate()
    {
        HandleCollision();
    }

    private void HandleCollision()
    {
        Vector3 desiredCamPos = transform.parent.TransformPoint(dollyDir * maxDistance) + Vector3.down;

        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCamPos, out hit, obstacleLayer))
        {

            distance = Mathf.Clamp((hit.distance * 0.5f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        Vector3 vel = Vector3.zero;
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, 0.2f);
        //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, dollyDir * distance, ref vel, 0.2f);
    }

    public void Zoom(float scroll)
    {
        float currentDistance = maxDistance;

        //float v = 0f;

        if (currentDistance <= maxZoomOut && currentDistance >= maxZoomIn)
        {
            // currentDistance = Mathf.SmoothDamp(currentDistance, currentDistance - scroll * scrollWheelSensitivity, ref v, 0.15f);
            currentDistance -= scroll * scrollWheelSensitivity;
        }

        currentDistance = Mathf.Clamp(currentDistance, maxZoomIn, maxZoomOut);
        maxDistance = currentDistance;
    }
}
