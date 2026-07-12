using UnityEngine;

public class CardData
{
    public ICardEffect CardEffect { get; private set; }
    public CombatAttribute CombatAttribute { get; private set; }
    public EffectActivateCondition EffectActivateCondition { get; private set; }

    public CardData(
        ICardEffect cardEffect, 
        CombatAttribute combatAttribute, 
        EffectActivateCondition effectActivateCondition)
    {
        CardEffect = cardEffect;
        CombatAttribute = combatAttribute;
        EffectActivateCondition = effectActivateCondition;
    }
}
