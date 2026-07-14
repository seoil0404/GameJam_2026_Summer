using System;
using UnityEngine;

public class TestCardEffect1 : CardEffectBase
{
    public override string Name => "Jab"; // ∫πΩÃ ¿Ï

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        opponent.Health -= 3;
        Debug.Log($"{owner.name} used {Name} on {opponent.name}, dealing 3 damage. {opponent.name}'s health is now {opponent.Health}.");
    }
}