using UnityEngine;
using System.Collections.Generic;

public class Enemy : Entity
{
    void Start()
    {

    }

    void Update()
    {

    }

    private CardData GenerateRandomCard()
    {
        CombatAttribute randomAttribute = (CombatAttribute)Random.Range(0, 3);
        EffectActivateCondition randomCondition = (EffectActivateCondition)Random.Range(0, 3);
        return new CardData(null, randomAttribute, randomCondition);
    }

    public List<CardData> GenerateRandomDeck(int count)
    {
        List<CardData> deck = new List<CardData>();

        for (int i = 0; i < count; i++)
        {
            CardData card = GenerateRandomCard();
            deck.Add(card);
        }

        return deck;
    }
}