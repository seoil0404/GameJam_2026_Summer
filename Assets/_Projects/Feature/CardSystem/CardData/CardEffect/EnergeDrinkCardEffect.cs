using UnityEngine;

public class EnergeDrinkCardEffect : CardEffectBase
{
    public override string Name => "EnergyDrink";

    public override int Priority => 0;

    public override string Description => "효과 : \n발동 시 체력을 2 회복합니다";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        owner.Health += CharacterAbilityUtility.ApplyJackpotChance(2);
        //Debug.Log($"{owner.name} used {Name}, restoring 5 health. {owner.name}'s health is now {owner.Health}.");
    }
}