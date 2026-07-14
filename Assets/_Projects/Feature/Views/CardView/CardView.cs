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
    [SerializeField] private Image resultView;
    [SerializeField] private CardViewAnimationController cardViewAnimationController;

    [Header("Registries")]
    [SerializeField] private CardEffectSpriteRegistry cardEffectSpriteRegistry;
    [SerializeField] private CombatAttributeIconRegistry combatAttributeIconRegistry;
    [SerializeField] private EffectActivateConditionIconRegistry effectActivateConditionIconRegistry;

    [Header("Prefabs")]
    [SerializeField] private CardDescriptionView cardDescriptionViewPrefab;

    public CardData CardData { get; private set; } = null;
    public bool bIsDragging { get; set; } = false;
    public CardViewAnimationController CardViewAnimationController => cardViewAnimationController;
    
    private ICardHoldView cardHoldView = null;

    private Canvas rootCanvas = null;
    private RectTransform rectTransform = null;
    private float moveLerpSpeed = 10.0f;
    private Vector2 targetPos = Vector2.zero;
    private float targetAngle = 0.0f;
    private float lerpedAngle = 0.0f;

    private CardDescriptionView cardDescriptionView = null;

    private CanvasGroup canvasGroup;

    private int originalIndex = 0;

    private bool isFlipped = false;

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

    public void SetResultView(EffectActivateCondition result)
    {
        resultView.sprite =
            effectActivateConditionIconRegistry.GetIcon(result);
    }

    public void AttatchCard(ICardHoldView _cardHoldView)
    {
        if(cardHoldView != _cardHoldView && _cardHoldView is FieldSlotView)
        {
            bool isFull = true;
            foreach (var fieldSlot in FieldManager.Instance.PlayerFieldSlotViews)
            {
                if (fieldSlot.CardView == null)
                {
                    isFull = false;
                    break;
                }
            }

            var _fieldSlotView = _cardHoldView as FieldSlotView;

            if (!isFull && _fieldSlotView.CardView != null)
                return;
        }

        CardViewAnimationController.OnAttatch();

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
        if(CardData.OwnerType == OwnerType.Enemy)
        {
            if (_cardHoldView is FieldSlotView)
            {
                transform.SetAsLastSibling();
            }
        }
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

    public void SetTransform(Vector2 vector, float zAngle = 0.0f)
    {
        targetPos = vector;
        targetAngle = zAngle;
    }

    private void LateUpdate()
    {
        if (!bIsDragging)
        {
            rectTransform.anchoredPosition = 
                Vector2.Lerp(rectTransform.anchoredPosition, targetPos, moveLerpSpeed * Time.deltaTime);

            if(PlayerStateBridge.bIsAllocating)
            {
                lerpedAngle =
                    Mathf.LerpAngle(lerpedAngle, targetAngle, moveLerpSpeed * Time.deltaTime);

                rectTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, lerpedAngle);
            }
        }
        if(descriptionGuideView.color == Color.white && Input.GetKeyDown(KeyCode.Tab))
        {
            if (cardDescriptionView == null)
            {
                Instantiate(cardDescriptionViewPrefab).SetDescriptionView(CardData);
            }
        }

        if (!PlayerStateBridge.bIsAllocating)
            lerpedAngle = 0;
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

        CardViewAnimationController.bIsTab = false;

        CardViewAnimationController.OnCardPickUp();

        originalIndex = transform.GetSiblingIndex();

        transform.SetAsLastSibling();

        CardPickManager.Instance.CardPicked(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        bIsDragging = false;
        canvasGroup.blocksRaycasts = true;

        CardViewAnimationController.OnCardPickDown();

        transform.SetSiblingIndex(originalIndex);

        CardPickManager.Instance.CardUnpicked(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        HandManager.Instance.PlayerHandView.OnEnterPointer(this);
        CardViewAnimationController.bIsTab = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!PlayerStateBridge.bIsAllocating || CardData.OwnerType == OwnerType.Enemy)
            return;

        HandManager.Instance.PlayerHandView.OnExitPointer(this);
        CardViewAnimationController.bIsTab = false;

    }

    public void Destroy()
    {
        CardViewAnimationController.Destroy();
    }
}