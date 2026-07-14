using System.Collections.Generic;
using UnityEngine;

public class CardVisualSynchrolyzer : MonoBehaviour
{
    public static CardVisualSynchrolyzer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SyncPlayer()
    {
        List<CardData> handCards = new();
        foreach(var cardView in HandManager.Instance.PlayerHandView.CardViews)
        {
            handCards.Add(cardView.CardData);
        }

        HandManager.Instance.PlayerHands = handCards;

        CardData[] cardDatas = new CardData[4];
        for(int index = 0; index < cardDatas.Length; index++)
        {
            cardDatas[index] = FieldManager.Instance.PlayerFieldSlotViews[index].CardView.CardData;
        }

        FieldManager.Instance.PlayerField.Cards = cardDatas;
    }

    public void SyncEnemy()
    {
        List<CardView> cardViews = new();

        foreach (var cardView in HandManager.Instance.EnemyHandView.CardViews)
            cardViews.Add(cardView);
        foreach (var fieldslotView in FieldManager.Instance.EnemyFieldSlotViews)
            cardViews.Add(fieldslotView.CardView);

        List<CardData> cardDatas = new();

        foreach(var cardData in HandManager.Instance.EnemyHands)
            cardDatas.Add(cardData);
        foreach(var cardData in FieldManager.Instance.PlayerField.Cards)
            cardDatas.Add(cardData);


    }
}
