using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Text healthText;

    public void SetHealthText(int health)
    {
        healthText.text = "x" + health.ToString();
    }
}