using TMPro;
using UnityEngine;

public class Timer : ManagedBehavior
{
    public TextMeshProUGUI TimerText;
    private float _elapsedTime;

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnLateUpdate()
    {

    }

    public override void OnUpdate()
    {
        _elapsedTime += Time.deltaTime;
        TimerText.text = DisplayTime(_elapsedTime);
    }

    private string DisplayTime(float time)
    {
        int seconds = (int)(time % 60);
        int minutes = (int)(time / 60) % 60;
        int hours = (int)(time / 3600) % 24;

        if (hours <= 0)
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        else
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }


    public string GetTime() 
    {
        return DisplayTime(_elapsedTime);
    }

}
