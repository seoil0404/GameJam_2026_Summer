using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [SerializeField] private Transform playerCardGenerateAnchor;
    [SerializeField] private Transform enemyCardGenerateAnchor;
    [SerializeField] private Transform playerCardGeneratePoint;
    [SerializeField] private Transform enemyCardGeneratePoint;
    [SerializeField] private CardView cardViewPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private List<Func<ICardEffect>> cardEffects { get; } = new List<Func<ICardEffect>>() 
    {
        () => new JabCardEffect(),
        () => new SlashCardEffect(),
        () => new EnergeDrinkCardEffect(),
        () => new MultivitaminCardEffect(),
    };

    private List<float> cardWeights { get; } = new List<float>() 
    { 30, 23, 17, 8 };

    public List<CardData> GenerateCards(int count, OwnerType owner)
    {
        List<CardData> result = new();

        for(int index = 0; index < count; index++)
        {
            int selectedIndex = GetWeightedRandomIndex(cardWeights);
            ICardEffect cardEffect = cardEffects[selectedIndex]?.Invoke() ?? null;
            CombatAttribute combatAttribute = GetRandomEnum<CombatAttribute>();
            EffectActivateCondition effectActivateCondition = GetRandomEnum<EffectActivateCondition>();

            CardData cardData = new(cardEffect, combatAttribute, effectActivateCondition, owner);
            result.Add(cardData);
        }

        return result;
    }

    private int GetWeightedRandomIndex(List<float> weights)
    {
        float totalWeight = 0f;
        for (int i = 0; i < weights.Count; i++)
            totalWeight += weights[i];

        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float cumulative = 0f;
        for (int i = 0; i < weights.Count; i++)
        {
            cumulative += weights[i];
            if (randomValue < cumulative)
                return i;
        }
        return weights.Count - 1;
    }

    public CardView GenerateCardView(CardData cardData)
    {
        Transform anchor = cardData.OwnerType == OwnerType.Player ? playerCardGenerateAnchor : enemyCardGenerateAnchor;
        var cardView = Instantiate(cardViewPrefab, anchor.transform);
        cardView.SetCardView(cardData);
        cardView.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        AudioManager.Instance.PlaySFX("3-1");

        cardData.CardEffect.SetCardView(cardView);
        
        if(cardData.OwnerType == OwnerType.Player)
        {
            cardView.transform.position = playerCardGeneratePoint.transform.position;
        }
        else if (cardData.OwnerType == OwnerType.Enemy)
        {
            cardView.transform.position = enemyCardGeneratePoint.transform.position;
        }

        return cardView;
    }

    private static T GetRandomEnum<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));

        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        return (T)values.GetValue(randomIndex);
    }
}
