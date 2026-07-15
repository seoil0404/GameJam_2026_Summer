using UnityEngine;

public class MultivitaminCardEffect : CardEffectBase
{
    public override string Name => "Multivitamin"; //종합비타민

    public override int Priority => 0;

    public override string Description => "효과 : \n 발동 시 잃은 체력의 20%를 회복합니다";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        int healthAmount = (20 - owner.Health) / 5;
        owner.Health += CharacterAbilityUtility.ApplyJackpotChance(healthAmount);
        EffectToOwner(owner, opponent);
    }
}