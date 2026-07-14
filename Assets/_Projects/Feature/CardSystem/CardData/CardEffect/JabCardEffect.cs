using System;
using UnityEngine;

public class JabCardEffect : CardEffectBase
{
    public override string Name => "Jab"; // 복싱 잽

    public override int Priority => 0;

    public override string Description => "효과 : \n발동 시 상대에게 3의 피해를 가합니다";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        opponent.Health -= 3;
        //Debug.Log($"{owner.name} used {Name} on {opponent.name}, dealing 3 damage. {opponent.name}'s health is now {opponent.Health}.");
    }
}