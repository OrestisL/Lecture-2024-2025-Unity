using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Range(0f, 1f)]
    public float CurrentAmount;
    public Image HealthBarImg;
    public Gradient HealthBarColor;


    private void Update()
    {
        HealthBarImg.color = HealthBarColor.Evaluate(CurrentAmount);
        HealthBarImg.fillAmount = CurrentAmount; // currentHealth / maxHealth
    }

}
