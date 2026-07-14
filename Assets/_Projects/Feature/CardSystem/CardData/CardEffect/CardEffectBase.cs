using System;
using UnityEngine;
using UnityEngine.UI;

public interface ICardEffect
{
    string Name { get; }
    string Description { get; }
    int Priority { get; }
    void ActivateEffect(Entity owner, Entity opponent);
}

public abstract class CardEffectBase : ICardEffect
{
    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract int Priority { get; }

    public abstract void ActivateEffect(Entity owner, Entity opponent);
}

