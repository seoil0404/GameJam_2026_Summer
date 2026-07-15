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
        if (!IsFieldFull())
            return;

        if (PlayerStateBridge.bIsAllocating)
        {
            PlayerStateBridge.AllocateComplete();
        }
    }

    private void Update()
    {
        if (IsFieldFull() && PlayerStateBridge.bIsAllocating)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    // FieldManager나 필드 슬롯이 아직 준비 안 됐을 수 있어서(씬 전환 직후 등) null 체크 후 순회한다.
    private bool IsFieldFull()
    {
        if (FieldManager.Instance == null || FieldManager.Instance.PlayerFieldSlotViews == null)
            return false;

        foreach (var item in FieldManager.Instance.PlayerFieldSlotViews)
        {
            if (item == null || item.CardView == null)
            {
                return false;
            }
        }

        return true;
    }
}
