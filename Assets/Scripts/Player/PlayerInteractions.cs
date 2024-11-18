using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInteractions : MonoBehaviour
{
    public LayerMask InteractableLayer;
    public float InteractRadius = 2.0f;
    private InputAction interactAction;

    private void Start()
    {
        SetInteractAction();
    }

    public void SetInteractAction()
    {
        interactAction = InputSystem.actions.FindAction("Interact");

        interactAction.started += (context) =>
        {
            Interact();
        };
    }

    private void Interact()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, InteractRadius, InteractableLayer);

        if (interactables.Length == 0) return;

        interactables =
            interactables.OrderByDescending(
                collider => Vector3.Distance(collider.gameObject.transform.position, transform.position)).ToArray();

        if (interactables[0].gameObject.TryGetComponent(out BaseInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
