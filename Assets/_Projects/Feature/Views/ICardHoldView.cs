using UnityEngine;

public interface ICardHoldView
{
    public void AllocateCard(CardView cardView);
    public void DeallocateCard(CardView cardView);
}
