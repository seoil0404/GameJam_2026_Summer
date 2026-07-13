using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
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

    public CardData CardData { get; private set; } = null;
    public bool bIsPicked { get; set; } = false;

    private ICardHoldView cardHoldView = null;

    private RectTransform rectTransform = null;
    private float moveLerpSpeed = 50.0f;
    private Vector2 targetPos = Vector2.zero;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void AttatchCard(ICardHoldView _cardHoldView)
    {
        if (cardHoldView != null)
            cardHoldView.DeallocateCard(this);

        cardHoldView.AllocateCard(this);
    }

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

    public void SetTransform(Vector2 vector)
    {
        targetPos = vector;
    }

    public void ForceSetTransform(Vector2 vector)
    {
        rectTransform.anchoredPosition = vector;
    }

    private void Update()
    {
        if (bIsPicked)
        {
            rectTransform.anchoredPosition = 
                Vector2.Lerp(rectTransform.anchoredPosition, targetPos, moveLerpSpeed * Time.deltaTime);
        }
    }
}