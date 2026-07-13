using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HandView : MonoBehaviour, ICardHoldView
{
    [SerializeField] private CardView cardViewPrefab;
    [SerializeField] private bool bIsInteractable;
    [SerializeField] private float radius;
    [SerializeField] private float interval;
    [SerializeField] private float cardLerpSpeed;

    private List<CardView> cardViews = new();
    private List<Vector2> cardViewTargetPos = new();

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void AllocateCard(CardView cardView)
    {
        cardViews.Add(cardView);
        RebuildCardView();
    }

    public void DeallocateCard(CardView cardView)
    {
        cardViews.Remove(cardView);
        RebuildCardView();
    }

    private void RebuildCardView()
    {
        int cardCount = cardViews.Count;
        cardViewTargetPos = new List<Vector2>(cardCount);

        float angleUnit = interval / radius;
        float offset = cardCount % 2 ==0 ? -angleUnit / 2 : 0;

        for (int index = -(cardCount / 2) - (cardCount % 2 - 1); index < cardCount/2; index++)
        {
            Vector2 pos = new(
                Mathf.Sin(angleUnit*index) + rectTransform.anchoredPosition.x, 
                Mathf.Cos(angleUnit*index) + rectTransform.anchoredPosition.y);

            cardViewTargetPos.Add(pos);
        }
    }

    private void Update()
    {
        for(int index = 0; index < cardViews.Count; index++)
        {
            RectTransform cardViewRectTransform = cardViews[index].GetComponent<RectTransform>();
            throw new NotImplementedException();
        }
    }
}
