using UnityEngine;

public class SlashCardEffect : CardEffectBase
{
    public override string Name => "Slash"; //참격

    public override int Priority => 0;

    public override string Description => "효과 : \n 발동 시 상대 체력의 20%의 피해를 가합니다";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        int slashDamage = opponent.Health / 5;
        opponent.Health -= CharacterAbilityUtility.ApplyJackpotChance(slashDamage);
        EffectToOpponent(owner, opponent);
        //Debug.Log($"{opponent.name}used {Name}, {opponent.Health} dealing  5 damage");
    }
}