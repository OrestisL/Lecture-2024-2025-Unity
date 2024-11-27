using UnityEngine;

public class SwitchInteractable : BaseInteractable
{
    public Light Light;
    public AnimationsController AnimationsController;

    public override void Start()
    {
        base.Start();
        //Light = transform.parent.GetComponentInChildren<Light>(true);
        AnimationsController = GetComponent<AnimationsController>();
        //AnimationsController.Init();
    }
    public override void Interact()
    {
        Light.enabled = !Light.enabled;
        AnimationsController.SetAnimatorBoolParameter("Press", Light.enabled);
    }

    public void ResetAnimParamStatus(string press) 
    {
        AnimationsController.SetAnimatorBoolParameter(press, false);
    }
}
