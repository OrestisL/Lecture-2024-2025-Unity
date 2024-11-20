using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    public float InputSensitivity;
    public CharacterController Controller;

    public float WalkSpeed, RunSpeed;
    private bool _running;
    public float Gravity;
    public float JumpHeight;
    private float _jumpSpeed;

    private Camera _mainCam;
    private float _rotX, _rotY;
    public float ClampAngle;

    private InputAction _lookAction;
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _jumpAction;

    private float turnSmoothVelocity, turnSmoothTime = 0.1f;

    private Vector3 _velocity;
    private Transform _model;
    private AnimationsController _animController;

    private void Start()
    {
        _mainCam = Camera.main;

        Controller = GetComponent<CharacterController>();

        _lookAction = InputSystem.actions.FindAction("Look");
        _moveAction = InputSystem.actions.FindAction("Move");

        _runAction = InputSystem.actions.FindAction("Sprint");
        _runAction.started += (_) => _running = true;
        _runAction.canceled += (_) => _running = false;

        _jumpAction = InputSystem.actions.FindAction("Jump");
        _jumpAction.started += (_) => Jump();

        _model = transform.GetChild(0);
        _animController = GetComponent<AnimationsController>();
    }

    private void Update()
    {
        Vector2 look = _lookAction.ReadValue<Vector2>();
        CameraRotation(look);

        Vector2 move = _moveAction.ReadValue<Vector2>();
        Move(move);

        ApplyGravity();
    }

    public void SetStartingValues(float x, float y)
    {
        _rotX = x;
        _rotY = y;
    }

    private void CameraRotation(Vector2 delta)
    {
        _rotY += delta.x * InputSensitivity * Time.deltaTime;
        _rotX -= delta.y * InputSensitivity * Time.deltaTime;

        //Clamp rotation x between -clampAngle and clampAngle
        _rotX = Mathf.Clamp(_rotX, -ClampAngle, ClampAngle);

        Quaternion localRot = Quaternion.Euler(_rotX, _rotY, 0.0f);
        _mainCam.transform.localRotation = localRot;
        _model.transform.localRotation = Quaternion.AngleAxis(_rotY, Vector3.up);
    }

    private void Move(Vector2 movement)
    {
        if (movement.sqrMagnitude == 0)
        {
            _animController.SetAnimatorFloatParameter("Speed", 0);
            return;
        }

        float currentSpeed = _running ? RunSpeed : WalkSpeed;
        _animController.SetAnimatorFloatParameter("Speed", currentSpeed);
        Vector3 inputDirection = new Vector3(movement.x, _jumpSpeed, movement.y).normalized;
        // apply camera rotation to player 
        float targetAngle = _mainCam.transform.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * currentSpeed;

        Controller.Move(Time.deltaTime * moveDirection);
    }

    private void ApplyGravity()
    {
        Controller.Move(_velocity * Time.deltaTime);
        _animController.SetAnimatorBoolParameter("IsGrounded", Controller.isGrounded);
        if (Controller.isGrounded && _velocity.y < 0)
            _velocity.y = Gravity;
        else if (!Controller.isGrounded)
            _velocity.y += Gravity * Time.deltaTime;
    }

    private void Jump()
    {
        if (!Controller.isGrounded) return;

        _velocity.y = Mathf.Sqrt(-2 * Gravity * JumpHeight);
    }
}
