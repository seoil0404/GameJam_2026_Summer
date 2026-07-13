using UnityEngine;

public interface ICardHoldView
{
    public void AllocateCard(CardView cardView);
    public void DeallocateCard(CardView cardView);
}

public abstract class CardHoldView : MonoBehaviour, ICardHoldView
{
    public abstract void AllocateCard(CardView cardView);

    public abstract void DeallocateCard(CardView cardView);
}