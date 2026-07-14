using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class CardView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Elements")]
    [SerializeField] private Image cardEffectView;
    [SerializeField] private Image combatAttributeIconView;
    [SerializeField] private Image effectActivateConditionIconView;
    [SerializeField] private Image descriptionGuideView;

    [Header("Registries")]
    [SerializeField] private CardEffectSpriteRegistry cardEffectSpriteRegistry;
    [SerializeField] private CombatAttributeIconRegistry combatAttributeIconRegistry;
    [SerializeField] private EffectActivateConditionIconRegistry effectActivateConditionIconRegistry;

    [Header("Prefabs")]
    [SerializeField] private CardDescriptionView cardDescriptionViewPrefab;

    public CardData CardData { get; private set; } = null;
    public bool bIsDragging { get; set; } = false;
    
    private ICardHoldView cardHoldView = null;

    private Canvas rootCanvas = null;
    private RectTransform rectTransform = null;
    private float moveLerpSpeed = 10.0f;
    private Vector2 targetPos = Vector2.zero;

    private CardDescriptionView cardDescriptionView = null;

    private CanvasGroup canvasGroup;

    private int originalIndex = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        descriptionGuideView.color = new Color(1, 1, 1, 0);
    }

    public void AttatchCard(ICardHoldView _cardHoldView)
    {
        if (cardHoldView == _cardHoldView)
            return;

        if (cardHoldView != null)
        {
            cardHoldView.DeallocateCard(this);
            if(cardHoldView is FieldSlotView && _cardHoldView is FieldSlotView)
            {
                var fieldSlotView = cardHoldView as FieldSlotView;
                var _fieldSlotView = _cardHoldView as FieldSlotView;

                var opponentCardView = _fieldSlotView.CardView;
                if(opponentCardView != null)
                {
                    opponentCardView.cardHoldView = fieldSlotView;
                    _fieldSlotView.DeallocateCard(opponentCardView);
                    fieldSlotView.AllocateCard(opponentCardView);
                }
            }
            else if(cardHoldView is HandView && _cardHoldView is FieldSlotView)
            {
                var _fieldSlotView = _cardHoldView as FieldSlotView;
                if(_fieldSlotView.CardView != null)
                {
                    _fieldSlotView.CardView.Destroy();
                }
            }
        }

        cardHoldView = _cardHoldView;

        cardHoldView.AllocateCard(this);

        if (_cardHoldView is FieldSlotView && CardData.OwnerType == OwnerType.Player)
            Player.Instance.OnAllocated();
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

    private void Update()
    {
        if (!bIsDragging)
        {
            rectTransform.anchoredPosition = 
                Vector2.Lerp(rectTransform.anchoredPosition, targetPos, moveLerpSpeed * Time.deltaTime);
        }
        if(descriptionGuideView.color == Color.white && Input.GetKeyDown(KeyCode.Tab))
        {
            if (cardDescriptionView == null)
                Instantiate(cardDescriptionViewPrefab);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        rectTransform.anchoredPosition += eventData.delta / rootCanvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        bIsDragging = true;
        canvasGroup.blocksRaycasts = false;

        descriptionGuideView.color = new Color(1, 1, 1, 0);

        CardPickManager.Instance.CardPicked(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        bIsDragging = false;
        canvasGroup.blocksRaycasts = true;

        descriptionGuideView.color = new Color(1, 1, 1, 1);

        CardPickManager.Instance.CardUnpicked(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        originalIndex = transform.GetSiblingIndex();

        transform.SetAsLastSibling();

        descriptionGuideView.color = new Color(1, 1, 1, 1);

        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        transform.SetSiblingIndex(originalIndex);

        descriptionGuideView.color = new Color(1, 1, 1, 0);

        transform.localScale = Vector3.one;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}