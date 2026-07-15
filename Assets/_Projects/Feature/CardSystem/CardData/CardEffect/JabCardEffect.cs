using System;
using UnityEngine;

public class JabCardEffect : CardEffectBase
{
    public override string Name => "Jab";

    public override int Priority => 0;

    public override string Description => "효과 : \n 발동 시 상대에게 3의 데미지를 입힙니다";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        opponent.Health -= CharacterAbilityUtility.ApplyJackpotChance(3, owner);
        EffectToOpponent(owner, opponent);
        //Debug.Log($"{owner.name} used {Name} on {opponent.name}, dealing 3 damage. {opponent.name}'s health is now {opponent.Health}.");
    }
}