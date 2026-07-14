using UnityEngine;
using UnityEngine.UI;

public class ExchangeStackView : MonoBehaviour
{
    [SerializeField] private Text exchangeStackText;

    public void SetStackView(int stack)
    {
        exchangeStackText.text = "교환 스택 : " + stack.ToString();
    }
}
