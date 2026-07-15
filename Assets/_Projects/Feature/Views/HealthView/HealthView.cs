using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject fullHealthWarningViewPrefab;

    public void SetHealthText(int health)
    {
        if(health == 20)
        {
            var obj = Instantiate(fullHealthWarningViewPrefab, transform);
            obj.transform.localPosition = Vector3.zero;
            Destroy(obj, 1.6f);
        }

        healthText.text = "x" + health.ToString();
    }
}