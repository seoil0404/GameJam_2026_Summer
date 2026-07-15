using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardVisualSynchronyzer : MonoBehaviour
{
    public static CardVisualSynchronyzer Instance { get; private set; }

    public event Action OnSyncEnemyComplete;

    [SerializeField] private EnemyStateView enemyStateView;

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
        StartCoroutine(SyncEnemyCoroutine());
    }

    private IEnumerator SyncEnemyCoroutine()
    {
        List<CardView> cardViews = new();

        foreach (var cardView in HandManager.Instance.EnemyHandView.CardViews)
            cardViews.Add(cardView);
        foreach (var fieldslotView in FieldManager.Instance.EnemyFieldSlotViews)
            cardViews.Add(fieldslotView.CardView);
        cardViews.RemoveAll(t => t == null);

        List<CardData> cardDatas = new();

        foreach (var cardData in HandManager.Instance.EnemyHands)
            cardDatas.Add(cardData);
        foreach (var cardData in FieldManager.Instance.EnemyField.Cards)
            cardDatas.Add(cardData);
        cardDatas.RemoveAll(t => t == null);

        foreach (var cardView in cardViews.ToArray())
        {
            int hash = cardView.CardData.Hash;
            var cardData = cardDatas.FirstOrDefault(t => t.Hash == hash);
            if (cardData == null)
            {
                cardViews.Remove(cardView);
                cardView.Destroy();
            }
        }

        foreach (var cardData in cardDatas)
        {
            int hash = cardData.Hash;
            var cardView = cardViews.FirstOrDefault(t => t.CardData.Hash == hash);
            if (cardView == null)
            {
                cardViews.Add(CardManager.Instance.GenerateCardView(cardData));
            }
        }

        for (int index = 0; index < HandManager.Instance.EnemyHands.Count; index++)
        {
            if (HandManager.Instance.EnemyHandView.CardViews.FirstOrDefault(t => t.CardData.Hash == HandManager.Instance.EnemyHands[index].Hash) == null)
            {
                var cardView = cardViews.Find(t => t.CardData.Hash == HandManager.Instance.EnemyHands[index].Hash);
                cardView.AttatchCard(HandManager.Instance.EnemyHandView);

                AudioManager.Instance.PlaySFX("4-1");

                yield return new WaitForSeconds(0.3f);
            }
        }

        for (int index = 0; index < FieldManager.Instance.EnemyFieldSlotViews.Length; index++)
        {
            if (FieldManager.Instance.EnemyFieldSlotViews[index].CardView == null)
            {
                var cardView = cardViews.Find(t => t.CardData.Hash == FieldManager.Instance.EnemyField.Cards[index].Hash);
                cardView.AttatchCard(FieldManager.Instance.EnemyFieldSlotViews[index]);

                AudioManager.Instance.PlaySFX("4-1");

                yield return new WaitForSeconds(0.3f);

                continue;
            }
            if (FieldManager.Instance.EnemyFieldSlotViews[index].CardView.CardData.Hash != FieldManager.Instance.EnemyField.Cards[index].Hash)
            {
                var cardView = cardViews.Find(t => t.CardData.Hash == FieldManager.Instance.EnemyField.Cards[index].Hash);
                cardView.AttatchCard(FieldManager.Instance.EnemyFieldSlotViews[index]);

                AudioManager.Instance.PlaySFX("4-1");

                yield return new WaitForSeconds(0.3f);
            }
        }

        int fireCount = 0;
        int waterCount = 0;
        int grassCount = 0;

        foreach (var cardData in HandManager.Instance.EnemyHands)
        {
            switch (cardData.CombatAttribute)
            {
                case CombatAttribute.Fire:
                    fireCount++;
                    break;
                case CombatAttribute.Water:
                    waterCount++;
                    break;
                case CombatAttribute.Grass:
                    grassCount++;
                    break;
                default:
                    break;
            }
        }

        enemyStateView.SetView(fireCount, waterCount, grassCount);

        OnSyncEnemyComplete?.Invoke();
    }
}
