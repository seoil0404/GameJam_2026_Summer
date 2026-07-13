using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardPutField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CardHoldView connectedCardHoldView;

    private Image image;

    public CardHoldView ConnectedCardHoldView => connectedCardHoldView;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.raycastTarget = false;
    }

    public void EnablePutField()
    {
        image.raycastTarget = true;
    }

    public void DisablePutField()
    {
        image.raycastTarget = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardPickManager.Instance.EnterCardPutField(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardPickManager.Instance.ExitCardPutField();
    }
}
