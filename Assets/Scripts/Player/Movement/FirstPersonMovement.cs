using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    public float inputSensitivity;
    public CharacterController Controller;

    public float WalkSpeed, RunSpeed;
    private bool _running;

    private Camera _mainCam;
    private float _rotX, _rotY;
    public float ClampAngle;

    private InputAction _lookAction;
    private InputAction _moveAction;
    private InputAction _runAction;

    private float turnSmoothVelocity, turnSmoothTime = 0.1f;
    private void Start()
    {
        _mainCam = Camera.main;

        Controller = GetComponent<CharacterController>();

        _lookAction = InputSystem.actions.FindAction("Look");
        _moveAction = InputSystem.actions.FindAction("Move");

        _runAction = InputSystem.actions.FindAction("Sprint");
        _runAction.started += (_) => _running = true;
        _runAction.canceled += (_) => _running = false;
    }

    private void Update()
    {
        Vector2 look = _lookAction.ReadValue<Vector2>();
        CameraRotation(look);

        Vector2 move = _moveAction.ReadValue<Vector2>();
        Move(move);
    }

    public void SetStartingValues(float x, float y)
    {
        _rotX = x;
        _rotY = y;
    }

    public void CameraRotation(Vector2 delta)
    {
        _rotY += delta.x * inputSensitivity * Time.deltaTime;
        _rotX -= delta.y * inputSensitivity * Time.deltaTime;

        //Clamp rotation x between -clampAngle and clampAngle
        _rotX = Mathf.Clamp(_rotX, -ClampAngle, ClampAngle);

        Quaternion localRot = Quaternion.Euler(_rotX, _rotY, 0.0f);
        _mainCam.transform.localRotation = localRot;
    }

    public void Move(Vector2 movement)
    {
        if (movement.sqrMagnitude == 0)
            return;

        float currentSpeed = _running ? RunSpeed : WalkSpeed;
        Vector3 inputDirection = new Vector3(movement.x, 0, movement.y).normalized;
        //apply camera rotation to player 
        float targetAngle = _mainCam.transform.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * currentSpeed;

        Controller.Move(Time.deltaTime * moveDirection);
    }
}
