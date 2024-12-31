using UnityEngine;
using UnityEngine.InputSystem;

public class AimDart : MonoBehaviour
{
    public float DistanceFromCamera = 10.0f;
    public GameObject DartPrefab;
    private GameObject _dartInstace;
    private Camera _cam;

    public float RotationSpeed = 10.0f;
    // force should be set from somewhere
    public float Force = 50.0f;

    private float _rotAngle;
    private Vector3 _currentDartPos;

    private void Start()
    {
        var mousePosAction = InputSystem.actions.FindAction("Point");
        mousePosAction.performed += Aim;

        _cam = Camera.main;

        var move = InputSystem.actions.FindAction("Move");
        move.performed += SetStartRotation;
        move.canceled += ResetRotation;

        var space = InputSystem.actions.FindAction("Jump");
        space.performed += InitThrow;

        var click = InputSystem.actions.FindAction("Click");
        click.performed += Throw;
    }

    private void Throw(InputAction.CallbackContext obj)
    {
        if (_dartInstace == null)
            return;
        _dartInstace.AddComponent<CleanupObject>().Init(30.0f);
        var movement = _dartInstace.AddComponent<DartMovement>();
        // initialize movement
        // _dartInstace.transform.rotation.eulerAngles.x is the current angle 
        movement.Init(Force);

        _dartInstace = null;

    }

    private void InitThrow(InputAction.CallbackContext obj)
    {
        // if it's not null, dont create another
        if (_dartInstace != null)
            return;

        _dartInstace = Instantiate(DartPrefab);
        _dartInstace.transform.position = _currentDartPos;
    }

    private void ResetRotation(InputAction.CallbackContext obj)
    {
        _rotAngle = 0;
    }

    private void Update()
    {
        if (_dartInstace == null)
            return;

        _dartInstace.transform.Rotate(Vector3.right, RotationSpeed * _rotAngle * Time.deltaTime);
    }

    private void SetStartRotation(InputAction.CallbackContext ctx)
    {
        _rotAngle -= ctx.ReadValue<Vector2>().y;
    }

    private void Aim(InputAction.CallbackContext ctx)
    {
        if (_dartInstace == null)
            return;

        Vector2 mousePos = ctx.ReadValue<Vector2>();
        // create a position in world space (3d space) from mouse position (in screen space)
        // by also using a distance from the camera
        _currentDartPos = _cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, DistanceFromCamera));

        _dartInstace.transform.position = _currentDartPos;
    }
}
