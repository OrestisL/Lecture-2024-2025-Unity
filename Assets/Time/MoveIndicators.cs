using System.Linq;
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

    public void SetTime(float hours, float minutes, float seconds)
    {
        _secondsAngle = seconds * 6.0f;
        _minutesAngle = minutes * 6.0f;
        _hoursAngle = hours * 30.0f;

        Seconds.transform.rotation = Quaternion.AngleAxis(_secondsAngle, -Vector3.forward);
        Minutes.transform.rotation = Quaternion.AngleAxis(_minutesAngle, -Vector3.forward);
        Hours.transform.rotation = Quaternion.AngleAxis(_hoursAngle, -Vector3.forward);

    }

    public void SetTime(string time)
    {
        //time should look like this hh:mm:ss
        if (time.Length > 8)
        {
            Debug.LogError($"Wrong input format for {nameof(time)}");
            return;
        }

        if (time.Length < 2 && !time.Contains(":"))
        {
            Debug.LogError($"Wrong input format for {nameof(time)}");
            return;
        }

        string[] parts = time.Split(":");
        int partsLength = parts.Length;
        if (partsLength > 3)
        {
            Debug.LogError($"Wrong input format for {nameof(time)}");
            return;
        }

        float hours = 0, minutes = 0,seconds = 0;
        switch (partsLength)
        {
            case 0:
                return;
            case 1:
                // only hours
                if (parts[0].Length > 2)
                {
                    Debug.LogError($"Wrong input format for {nameof(time)}");
                    return;
                }
                hours = float.Parse(parts[0]);
                break;
            case 2:
                //hours & minutes
                if (parts[0].Length > 2)
                {
                    Debug.LogError($"Wrong input format for {nameof(time)}");
                    return;
                }
                hours = float.Parse(parts[0]);
                if (parts[1].Length > 2)
                {
                    Debug.LogError($"Wrong input format for {nameof(time)}");
                    return;
                }
                minutes = float.Parse(parts[1]);
                break;
            case 3:
                //everything
                if (parts[0].Length > 2)
                {
                    Debug.LogError($"Wrong input format for {nameof(time)}");
                    return;
                }
                hours = float.Parse(parts[0]);
                if (parts[1].Length > 2)
                {
                    Debug.LogError($"Wrong input format for {nameof(time)}");
                    return;
                }
                minutes = float.Parse(parts[1]);
                if (parts[2].Length > 2)
                {
                    Debug.LogError($"Wrong input format for {nameof(time)}");
                    return;
                }
                seconds = float.Parse(parts[2]);
                break;
        }

        SetTime(hours, minutes, seconds);

    }
}
