using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public LayerMask InteractableLayer;
    public float InteractRadius = 2.0f;
    private InputAction interactAction;

    public void SetAction(InputAction action)
    {
        interactAction = action;

        interactAction.started += (context) => 
        {
            Collider[] interactables = Physics.OverlapSphere(transform.position, 
                InteractRadius, InteractableLayer);

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
