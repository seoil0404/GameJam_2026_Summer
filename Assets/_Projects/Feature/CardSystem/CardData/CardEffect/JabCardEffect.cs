using System;
using UnityEngine;

public class JabCardEffect : CardEffectBase
{
    public override string Name => "Jab"; // º¹½Ì Àì

    public override int Priority => 0;

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        opponent.Health -= CharacterAbilityUtility.ApplyJackpotChance(3);
        //Debug.Log($"{owner.name} used {Name} on {opponent.name}, dealing 3 damage. {opponent.name}'s health is now {opponent.Health}.");
    }
}