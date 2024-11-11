using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController Controller;
    public float WalkSpeed;
    public float RunSpeed;
    private bool _running;

    private InputAction _moveAction;
    private InputAction _runAction;
    [SerializeField]
    private Camera _mainCam;

    private float turnSmoothVelocity, turnSmoothTime = 0.1f;
    private void Start()
    {
        Controller = GetComponent<CharacterController>();

        _moveAction = InputSystem.actions.FindAction("Move");

        _runAction = InputSystem.actions.FindAction("Sprint");
        _runAction.started += (_) => _running = true;
        _runAction.canceled += (_) => _running = false;

        _mainCam = Camera.main;
    }

    private void Update()
    {
        Vector2 move = _moveAction.ReadValue<Vector2>();
        Move(move);
    }

    private void Move(Vector2 input)
    {
        if (input.sqrMagnitude == 0)
            return;

        Vector3 inputDirection = new Vector3(input.x, 0, input.y).normalized;

        //rotate player to where the camera is looking
        float targetAngle = _mainCam.transform.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.localRotation = Quaternion.Euler(0, angle, 0);

        float currentSpeed = _running ? RunSpeed : WalkSpeed; 

        //rotate forward to look direction
        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * currentSpeed;

        Controller.Move(Time.deltaTime * moveDirection);
    }
}
