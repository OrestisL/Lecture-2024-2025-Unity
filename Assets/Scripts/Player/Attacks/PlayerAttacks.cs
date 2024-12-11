using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    private AnimationsController animationsController;
    public float PunchCooldown = 5.0f;

    private float _cooldown = 0.0f;

    private void Start()
    {
       animationsController = GetComponent<AnimationsController>();
        var leftClick = InputSystem.actions.FindAction("Click");

        leftClick.performed += Punch;
    }

    private void Punch(InputAction.CallbackContext obj)
    {
        if (_cooldown <= 0.0f) 
        {
            animationsController.SetAnimatorBoolParameter("Punch", true);
            _cooldown = PunchCooldown;
        }
    }

    private void Update()
    {
        _cooldown -= Time.deltaTime;
    }
}
