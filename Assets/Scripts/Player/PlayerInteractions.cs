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
        SetAction();
    }

    public void SetAction()
    {
        interactAction = InputSystem.actions.FindAction("Interact");

        interactAction.started += (context) => 
        {
            Collider[] interactables = Physics.OverlapSphere(transform.position, 
                InteractRadius, InteractableLayer);

            interactables = 
                interactables.OrderByDescending(
                    collider => Vector3.Distance(collider.gameObject.transform.position, transform.position)).ToArray();

            if (interactables.Length > 0) 
            {
                if (interactables[0].gameObject.TryGetComponent(out BaseInteractable interactable)) 
                {
                    interactable.Interact();
                }
            }
        };
    }



}
