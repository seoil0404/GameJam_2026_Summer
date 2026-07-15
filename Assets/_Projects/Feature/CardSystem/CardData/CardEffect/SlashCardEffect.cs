using UnityEngine;

public class SlashCardEffect : CardEffectBase
{
    public override string Name => "Slash";

    public override int Priority => 0;

    public override string Description => "효과 : \n 발동 시 상대 체력에 20%에 해당하는 데미지를 입힙니다";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        int slashDamage = opponent.Health / 5;
        opponent.Health -= CharacterAbilityUtility.ApplyJackpotChance(slashDamage, owner);
        EffectToOpponent(owner, opponent);
        //Debug.Log($"{opponent.name}used {Name}, {opponent.Health} dealing  5 damage");
    }
}