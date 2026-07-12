using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer cardEffectView;
    [SerializeField] private SpriteRenderer combatAttributeIconView;
    [SerializeField] private SpriteRenderer effectActivateConditionIconView;

    [Header("Registries")]
    [SerializeField] private CardEffectSpriteRegistry cardEffectSpriteRegistry;
    [SerializeField] private CombatAttributeIconRegistry combatAttributeIconRegistry;
    [SerializeField] private EffectActivateConditionIconRegistry effectActivateConditionIconRegistry;

    public void SetCardView(CardData card)
    {
        cardEffectView.sprite = 
            cardEffectSpriteRegistry.GetSprite(card.CardEffect.Name);

        combatAttributeIconView.sprite = 
            combatAttributeIconRegistry.GetIcon(card.CombatAttribute);

        effectActivateConditionIconView.sprite = 
            effectActivateConditionIconRegistry.GetIcon(card.EffectActivateCondition);
    }
}