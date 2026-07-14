using System;
using UnityEngine;

[Serializable]
public enum OwnerType
{
    Player,
    Enemy
}

[Serializable]
public class CardData
{
    private static int hashStack = 0;

    public ICardEffect CardEffect { get; private set; }
    public CombatAttribute CombatAttribute { get; private set; }
    public EffectActivateCondition EffectActivateCondition { get; private set; }
    public OwnerType OwnerType { get; private set; }
    public int Hash { get; private set; }

    public CardData(
        ICardEffect cardEffect, 
        CombatAttribute combatAttribute, 
        EffectActivateCondition effectActivateCondition,
        OwnerType ownerType)
    {
        CardEffect = cardEffect;
        CombatAttribute = combatAttribute;
        EffectActivateCondition = effectActivateCondition;
        OwnerType = ownerType;
        Hash = hashStack++;
    }
}
