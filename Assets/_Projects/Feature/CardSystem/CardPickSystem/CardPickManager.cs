using UnityEngine;

public class CardPickManager : MonoBehaviour
{
    public static CardPickManager Instance { get; private set; }

    [SerializeField] private CardPutField[] cardPutFields;

    private CardPutField currentCardPutField = null;

    private void Awake()
    {
        Instance = this;
    }

    public void CardPicked(CardView cardView)
    {
        EnableCardPutFields();
    }

    public void CardUnpicked(CardView cardView)
    {
        DisableCardPutFields();

        if(currentCardPutField != null)
        {
            cardView.AttatchCard(currentCardPutField.ConnectedCardHoldView);
        }
    }

    private void EnableCardPutFields()
    {
        foreach(var field in cardPutFields)
        {
            field.EnablePutField();
        }
    }

    private void DisableCardPutFields()
    {
        foreach (var field in cardPutFields)
        {
            field.DisablePutField();
        }
    }

    public void EnterCardPutField(CardPutField cardPutField)
    {
        currentCardPutField = cardPutField;
    }

    public void ExitCardPutField()
    {
        currentCardPutField = null;
    }
}
