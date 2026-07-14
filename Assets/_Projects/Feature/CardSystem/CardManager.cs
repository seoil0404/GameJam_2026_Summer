using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [SerializeField] private Transform generateAnchor;
    [SerializeField] private CardView cardViewPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private List<Func<ICardEffect>> cardEffects { get; } = new List<Func<ICardEffect>>() 
    {
        () => new TestCardEffect1()
    };

    public List<CardData> GenerateCards(int count, OwnerType owner)
    {
        List<CardData> result = new();

        for(int index = 0; index < count; index++)
        {
            ICardEffect cardEffect = cardEffects[UnityEngine.Random.Range(0, cardEffects.Count)]?.Invoke() ?? null;
            CombatAttribute combatAttribute = GetRandomEnum<CombatAttribute>();
            EffectActivateCondition effectActivateCondition = GetRandomEnum<EffectActivateCondition>();

            CardData cardData = new(cardEffect, combatAttribute, effectActivateCondition, owner);
            result.Add(cardData);
        }

        return result;
    }

    public CardView GenerateCardView(CardData cardData)
    {
        var cardView = Instantiate(cardViewPrefab, generateAnchor.transform);
        cardView.SetCardView(cardData);
        cardView.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        return cardView;
    }

    private static T GetRandomEnum<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));

        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        return (T)values.GetValue(randomIndex);
    }
}
