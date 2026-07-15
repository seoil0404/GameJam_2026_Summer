using System;
using UnityEngine;
using UnityEngine.UI;

public interface ICardEffect
{
    string Name { get; }
    string Description { get; }
    int Priority { get; }
    void SetCardView(CardView cardView);
    void ActivateEffect(Entity owner, Entity opponent);
}

public abstract class CardEffectBase : ICardEffect
{
    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract int Priority { get; }

    protected CardView CardView { get; set; }

    public abstract void ActivateEffect(Entity owner, Entity opponent);

    protected void EffectToOwner(Entity owner, Entity opponent)
    {
        Vector2 startPos = CardView.transform.position;
        Vector2 endPos = owner.HealthView.transform.position;

        ParticleManager.instance.PlayAttackTrail(startPos, endPos);
    }

    protected void EffectToOpponent(Entity owner, Entity opponent)
    {
        Vector2 startPos = CardView.transform.position;
        Vector2 endPos = opponent.HealthView.transform.position;

        ParticleManager.instance.PlayAttackTrail(startPos, endPos);
    }

    public void SetCardView(CardView cardView)
    {
        CardView = cardView;
    }
}

