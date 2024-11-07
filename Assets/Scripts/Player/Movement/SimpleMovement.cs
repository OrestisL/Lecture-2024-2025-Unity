using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed;

    [SerializeField]
    private InputAction _moveAction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 currentMoveValue = _moveAction.ReadValue<Vector2>();
        Move(new (currentMoveValue.x , 0 , currentMoveValue.y));
    }

    private void Move(Vector3 input) 
    {
        if (input.sqrMagnitude == 0)
            return;
        
        input.y = 0;
        controller.Move(Time.deltaTime * walkSpeed * input);
        //add code here for running
    }
}
