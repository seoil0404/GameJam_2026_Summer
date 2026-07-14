using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HandView : CardHoldView
{
    [SerializeField] private float interval;
    [SerializeField] private bool reverseDirection;

    private List<CardView> cardViews = new();
    private List<Vector2> cardViewTargetPos = new();

    private RectTransform rectTransform;

    public List<CardView> CardViews => cardViews;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public override void AllocateCard(CardView cardView)
    {
        cardViews.Add(cardView);
        RebuildCardView();
    }

    public override void DeallocateCard(CardView cardView)
    {
        cardViews.Remove(cardView);
        RebuildCardView();
    }

    private void RebuildCardView()
    {
        cardViews.RemoveAll(t => t == null);

        int cardCount = cardViews.Count;
        cardViewTargetPos = new List<Vector2>(cardCount);

        int multiplier = reverseDirection ? -1 : 1;

        for(int index = 0; index < cardCount; index++)
        {
            cardViewTargetPos.Add(
                new Vector2(
                    interval * (index - cardCount/2) * multiplier + rectTransform.anchoredPosition.x, 
                    rectTransform.anchoredPosition.y
                    )
                );
        }

        for (int index = 0; index < cardCount; index++)
        {
            int drawIndex = reverseDirection ? cardCount - 1 - index : index;
            cardViews[drawIndex].transform.SetAsLastSibling();
        }
    }

    private void Update()
    {
        for(int index = 0; index < cardViews.Count; index++)
        {
            cardViews[index].SetTransform(cardViewTargetPos[index]);
        }
    }
}
