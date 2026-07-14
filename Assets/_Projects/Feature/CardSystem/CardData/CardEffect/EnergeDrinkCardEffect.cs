using UnityEngine;

public class EnergeDrinkCardEffect : CardEffectBase
{
    public override string Name => "Energy_Drink";

    public override int Priority => 0;

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        owner.Health += CharacterAbilityUtility.ApplyJackpotChance(2);
        //Debug.Log($"{owner.name} used {Name}, restoring 5 health. {owner.name}'s health is now {owner.Health}.");
    }
}