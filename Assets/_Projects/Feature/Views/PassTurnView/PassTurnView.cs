using UnityEngine;
using UnityEngine.UI;

public class PassTurnView : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPressPassTurn()
    {
        bool isFull = true;
        foreach(var item in FieldManager.Instance.PlayerFieldSlotViews)
        {
            if(item.CardView == null)
            {
                isFull = false;
                break;
            }
        }

        if (isFull && PlayerStateBridge.bIsAllocating)
        {
            PlayerStateBridge.AllocateComplete();
        }
    }

    private void Update()
    {
        bool isFull = true;
        foreach (var item in FieldManager.Instance.PlayerFieldSlotViews)
        {
            if (item.CardView == null)
            {
                isFull = false;
                break;
            }
        }

        if (isFull && PlayerStateBridge.bIsAllocating)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
