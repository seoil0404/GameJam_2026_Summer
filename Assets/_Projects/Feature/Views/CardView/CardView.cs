using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image cardEffectView;
    [SerializeField] private Image combatAttributeIconView;
    [SerializeField] private Image effectActivateConditionIconView;

    [Header("Registries")]
    [SerializeField] private CardEffectSpriteRegistry cardEffectSpriteRegistry;
    [SerializeField] private CombatAttributeIconRegistry combatAttributeIconRegistry;
    [SerializeField] private EffectActivateConditionIconRegistry effectActivateConditionIconRegistry;

    public CardData CardData { get; private set; }

    public void SetCardView(CardData card)
    {
        CardData = card;

        cardEffectView.sprite = 
            cardEffectSpriteRegistry.GetSprite(card.CardEffect.Name);

        combatAttributeIconView.sprite = 
            combatAttributeIconRegistry.GetIcon(card.CombatAttribute);

        effectActivateConditionIconView.sprite = 
            effectActivateConditionIconRegistry.GetIcon(card.EffectActivateCondition);
    }
}