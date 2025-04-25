using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        
        float percent = (float)health / slider.maxValue;
        Image fillImage = slider.fillRect.GetComponent<Image>();

        if (percent > 0.5f)
            fillImage.color = Color.green;
        else
            fillImage.color = Color.red;
    }
}
