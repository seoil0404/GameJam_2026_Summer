using UnityEngine;

public class PassTurnView : MonoBehaviour
{
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
}
