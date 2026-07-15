using UnityEngine;
using UnityEngine.UI;

public class EnemyStateView : MonoBehaviour
{
    [SerializeField] private Text fireCountView;
    [SerializeField] private Text waterCountView;
    [SerializeField] private Text grassCountView;

    public void SetView(int fireCount, int waterCount, int grassCount)
    {
        fireCountView.text = fireCount.ToString();
        waterCountView.text = waterCount.ToString();
        grassCountView.text = grassCount.ToString();
    }
}
