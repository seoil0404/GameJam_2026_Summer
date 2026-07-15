using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HandView : CardHoldView
{
    [SerializeField] private float interval;
    [SerializeField] private float radius;
    [SerializeField] private bool reverseDirection;
    [SerializeField] private float selectedHighlightLength;

    private List<CardView> cardViews = new();
    private List<CardView> pointerEnterCardViews = new();
    private List<Vector2> cardViewTargetPos = new();
    private List<float> cardViewTargetAngle = new();

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

    public void OnEnterPointer(CardView cardView)
    {
        pointerEnterCardViews.Add(cardView);
        RebuildCardView();
    }

    public void OnExitPointer(CardView cardView)
    {
        pointerEnterCardViews.Remove(cardView);
        RebuildCardView();
    }

    private void RebuildCardView()
    {
        cardViews.RemoveAll(t => t == null);

        int cardCount = cardViews.Count;
        cardViewTargetPos = new List<Vector2>(cardCount);
        cardViewTargetAngle = new List<float>(cardCount);

        float multiplier = reverseDirection ? -1f : 1f;

        float _interval = interval * (-(1/14) * (float)cardCount + 3/2);

        for (int index = 0; index < cardCount; index++)
        {
            float additionalValue = 0.0f;
            if (pointerEnterCardViews.FirstOrDefault(t => cardViews[index].CardData.Hash == t.CardData.Hash) != null)
            {
                additionalValue += selectedHighlightLength;
            }

            float angle = _interval / radius;
            angle = angle * ((float)index - (float)cardCount * 0.5f + 0.5f);
            Vector2 targetPosition = new Vector2(Mathf.Sin(angle) * (radius + additionalValue), (Mathf.Cos(angle) * (radius + additionalValue) - radius) * multiplier);
            targetPosition += rectTransform.anchoredPosition;

            cardViewTargetPos.Add(targetPosition);
            cardViewTargetAngle.Add(-angle * Mathf.Rad2Deg * multiplier);
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
            cardViews[index].SetTransform(cardViewTargetPos[index], cardViewTargetAngle[index]);
        }
    }
}
