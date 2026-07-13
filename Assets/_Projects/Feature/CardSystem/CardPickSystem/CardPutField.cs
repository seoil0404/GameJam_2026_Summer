using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardPutField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ICardHoldView connectedCardHoldView;

    private Image image;

    public ICardHoldView ConnectedCardHoldView => connectedCardHoldView;

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

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
