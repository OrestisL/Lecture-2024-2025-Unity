using UnityEngine;

public class Interactable : BaseInteractable
{
    public override void Start()
    {
        base.Start();
        InteractRadius = 3.0f;
    }
    public override void Interact()
    {
        Debug.Log($"This is the {name} interactable!");
    }
}
