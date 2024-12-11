using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private AnimationsController animationsController;
    public GameObject PunchHitbox;
    private void Start()
    {
       animationsController = GetComponentInParent<AnimationsController>();
    }

    public void TestEvent() 
    {
        Debug.Log("Run!!!");
    }

    public void TogglePunchHitbox(int show) 
    {
        animationsController.SetAnimatorBoolParameter("Punch", false);
        PunchHitbox.SetActive(show == 0 ? true : false);
    }
}
