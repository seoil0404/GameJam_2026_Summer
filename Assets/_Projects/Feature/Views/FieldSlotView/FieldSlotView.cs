using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FieldSlotView : CardHoldView
{
    private RectTransform rectTransform;
    private CardView cardView = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public override void AllocateCard(CardView cardView)
    {
        this.cardView = cardView;
    }

    public override void DeallocateCard(CardView cardView)
    {
        if(this.cardView == cardView)
        {
            this.cardView = null;
        }
    }

    private void Update()
    {
        cardView?.SetTransform(rectTransform.anchoredPosition);
    }
}
