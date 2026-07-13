using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private List<Func<ICardEffect>> cardEffects { get; } = new List<Func<ICardEffect>>() 
    {
        () => new TestCardEffect1()
    };

    public List<CardData> GenerateCards(int count)
    {
        List<CardData> result = new();

        for(int index = 0; index < count; index++)
        {
            ICardEffect cardEffect = cardEffects[UnityEngine.Random.Range(0, cardEffects.Count)]?.Invoke() ?? null;
            CombatAttribute combatAttribute = GetRandomEnum<CombatAttribute>();
            EffectActivateCondition effectActivateCondition = GetRandomEnum<EffectActivateCondition>();

            CardData cardData = new(cardEffect, combatAttribute, effectActivateCondition);
            result.Add(cardData);
        }

        return result;
    }

    private static T GetRandomEnum<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));

        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        return (T)values.GetValue(randomIndex);
    }
}