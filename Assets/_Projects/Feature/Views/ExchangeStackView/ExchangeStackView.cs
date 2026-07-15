using UnityEngine;
using UnityEngine.UI;

public class ExchangeStackView : MonoBehaviour
{
    [SerializeField] private Text exchangeStackText;

    public void SetStackView(int stack)
    {
        exchangeStackText.text = "±³Ã¼ È½¼ö : " + stack.ToString();
    }
}
