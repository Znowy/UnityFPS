using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthText;

    public void UpdateHealthUI(float health)
    {
        healthText.text = healthText.text.Substring(0, 8) + Mathf.FloorToInt(health);
    }
}
