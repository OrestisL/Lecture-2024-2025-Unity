using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Vector3 _targetPosition;

    InputAction _mousePosition;
    private PatrolMovement _movement;
    private void Start()
    {
        var leftclick = InputSystem.actions.FindAction("Click");
        leftclick.performed += (_) =>
        {
            AssignAgent();
            SetTarget();
        };

        _mousePosition = InputSystem.actions.FindAction("Point");

        InputSystem.actions.FindAction("RightClick").performed += (_) => _agent = null;
    }

    private void AssignAgent()
    {
        Vector2 mousePos = _mousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (_agent == null)
            {
                if (hitInfo.transform.gameObject.
                    TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
                {
                    _agent = agent;
                }
                if (hitInfo.transform.gameObject.TryGetComponent<PatrolMovement>(out PatrolMovement movement)) 
                {
                    _movement = movement;
                }
                return;
            }

            if (_agent != null)
            {
                if (NavMesh.Raycast(transform.position, hitInfo.point, out NavMeshHit hit, NavMesh.AllAreas))
                {
                    _targetPosition = hit.position;
                }
            }
        }
    }

    private void SetTarget()
    {
        if (_agent == null) return;
        if (_movement == null) return;

        //_agent.SetDestination(_targetPosition);
        _movement.AddPosition(_targetPosition);
    }
}
