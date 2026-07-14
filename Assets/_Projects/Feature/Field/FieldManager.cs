using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    [field: SerializeField] public Field PlayerField { get; set; }
    [field: SerializeField] public Field EnemyField { get; set; }

    [field: SerializeField] public FieldSlotView[] PlayerFieldSlotViews { get; set; }
    [field: SerializeField] public FieldSlotView[] EnemyFieldSlotViews { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    public CardView GetCardViewOnField(CardData cardData)
    {
        foreach (var item in PlayerFieldSlotViews)
        {
            if (item.CardView.CardData.Hash == cardData.Hash)
                return item.CardView;
        }
        foreach (var item in EnemyFieldSlotViews)
        {
            if (item.CardView.CardData.Hash == cardData.Hash)
                return item.CardView;
        }

        return null;
    }
}
