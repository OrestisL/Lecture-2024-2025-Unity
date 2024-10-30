using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.onValueChanged.AddListener((newValue) => Time.timeScale = newValue);
    }

}
