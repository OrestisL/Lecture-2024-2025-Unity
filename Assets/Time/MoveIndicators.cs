using UnityEngine;

public class MoveIndicators : MonoBehaviour
{
    public GameObject Seconds, Minutes, Hours;

    float _secondsAngle;
    float _minutesAngle;
    float _hoursAngle;
    private void Start()
    {
        ResetIndicators();
    }

    private void Update()
    {
        float seconds = 60.0f / Time.deltaTime;
        float minutes = seconds * 60.0f;
        float hours = minutes * 60.0f;

        _secondsAngle += 360.0f / seconds;
        _minutesAngle += 360.0f / minutes;
        _hoursAngle += 360.0f / hours;

        Seconds.transform.rotation = Quaternion.AngleAxis(_secondsAngle, -Vector3.forward);
        Minutes.transform.rotation = Quaternion.AngleAxis(_minutesAngle, -Vector3.forward);
        Hours.transform.rotation = Quaternion.AngleAxis(_hoursAngle, -Vector3.forward);

    }

    private void ResetIndicators() 
    {
        Seconds.transform.rotation = Quaternion.identity;
        Minutes.transform.rotation = Quaternion.identity;
        Hours.transform.rotation = Quaternion.identity;
    }
}
