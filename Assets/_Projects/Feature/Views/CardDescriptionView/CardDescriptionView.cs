using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionView : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image cardEffectView;
    [SerializeField] private Image combatAttributeIconView;
    [SerializeField] private Image effectActivateConditionIconView;

    [Header("Registries")]
    [SerializeField] private CardEffectSpriteRegistry cardEffectSpriteRegistry;
    [SerializeField] private CombatAttributeIconRegistry combatAttributeIconRegistry;
    [SerializeField] private EffectActivateConditionIconRegistry effectActivateConditionIconRegistry;

    [SerializeField] private Text effectTitle;
    [SerializeField] private Text effectDescription;

    public void SetDescriptionView(CardData cardData)
    {
        cardEffectView.sprite =
            cardEffectSpriteRegistry.GetSprite(cardData.CardEffect.Name);

        combatAttributeIconView.sprite =
            combatAttributeIconRegistry.GetIcon(cardData.CombatAttribute);

        effectActivateConditionIconView.sprite =
            effectActivateConditionIconRegistry.GetIcon(cardData.EffectActivateCondition);

        effectTitle.text = cardData.CardEffect.Name;
        effectDescription.text = cardData.CardEffect.Description;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
            Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
