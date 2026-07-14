using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverScale = 3f;
    [SerializeField] private float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    // Update is called once per frame
    public void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }
}
